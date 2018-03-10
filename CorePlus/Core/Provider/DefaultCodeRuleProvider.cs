using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Core
{
    public class DefaultCodeRuleProvider : ICodeRuleProvider
    {
        public virtual string CodeRuleField
        {
            get { return @"\{(@?\w+)(:[\w,.: ]*)?\}|\<(@?\w+)(:[\w,.: ]*)?\>"; }
        }

        public IEnumerable<ICodeRuleEntity> GetCodeRules(string tableName, int type)
        {
            return BaseCache.CodeRule.Where(x => x.TableName == tableName && x.Type <= type);
        }

        public string GetCode(ICodeRuleEntity codeRule, object entity, bool isTemp)
        {
            var context = CoreDBContext.GetContext();
            string generateRule = AnalyseGenerateRule(codeRule.GenerateRule, entity);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter() { ParameterName = "@TableName", Value = codeRule.TableName, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50 });
            parameters.Add(new SqlParameter() { ParameterName = "@FieldName", Value = codeRule.FieldName, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50 });
            parameters.Add(new SqlParameter() { ParameterName = "@GenerateRule", Value = generateRule, SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100 });
            parameters.Add(new SqlParameter() { ParameterName = "@DateRule", Value = codeRule.DateRule ?? "", SqlDbType = System.Data.SqlDbType.NVarChar, Size = 50 });
            parameters.Add(new SqlParameter() { ParameterName = "@AutoIncreaceRule", Value = codeRule.AutoIncreaceRule ?? "", SqlDbType = System.Data.SqlDbType.NVarChar, Size = 10 });
            parameters.Add(new SqlParameter() { ParameterName = "@IsTemp", Value = isTemp, SqlDbType = System.Data.SqlDbType.Bit });
            parameters.Add(new SqlParameter() { ParameterName = "@OutValue", SqlDbType = System.Data.SqlDbType.NVarChar, Size = 100, Direction = System.Data.ParameterDirection.Output });
            context.Database.ExecuteSqlCommand("Exec dbo.SP_GetCodeByCodeRule @TableName,@FieldName,@GenerateRule,@DateRule,@AutoIncreaceRule,@IsTemp,@OutValue OUTPUT", parameters.ToArray());
            return Convert.ToString(parameters[6].Value);
        }

        private string AnalyseGenerateRule(string generateRule, object entity)
        {
            Regex regex = new Regex(CodeRuleField);
            Type type = entity.GetType();
            MatchEvaluator matchEvaluator = new MatchEvaluator(m =>
            {
                var fieldName = !string.IsNullOrEmpty(m.Groups[1].Value) ? m.Groups[1].Value : m.Groups[3].Value;
                var matchFormat = !string.IsNullOrEmpty(m.Groups[2].Value) ? m.Groups[2].Value : m.Groups[4].Value;
                if (!string.IsNullOrEmpty(fieldName) && fieldName != "date" && fieldName != "no")
                {
                    if (fieldName.StartsWith("@"))
                    {
                        return string.Format("{0" + matchFormat + "}", ParamManager.GetParamValue(fieldName));
                    }
                    else
                    {
                        PropertyInfo property = type.GetProperty(fieldName);
                        if (property == null)
                        {
                            throw new WarningException("解析编码表达式错误:不包含字段{0}", fieldName);
                        }
                        if (m.Groups[1].Value != "")
                        {
                            return string.Format("{0" + matchFormat + "}", property.GetValue(entity, null));
                        }
                        else
                        {
                            return string.Format("<{0" + matchFormat + "}>", property.GetValue(entity, null));
                        }
                    }
                }
                return m.Groups[0].Value;
            });
            return regex.Replace(generateRule, matchEvaluator);
        }
    }
}
