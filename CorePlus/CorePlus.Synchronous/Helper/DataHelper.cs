using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class DataHelper
    {
        private static void AppendData<TEntity>(TEntity entity, List<TEntity> list)
            where TEntity : class,new()
        {
            lock (ParamHelper.objLock)
            {
                list.Add(entity);
            }
        }

        private static void RemoveData<TEntity>(TEntity entity, List<TEntity> list)
            where TEntity : class,new()
        {
            lock (ParamHelper.objLock)
            {
                list.Remove(entity);
            }
        }

        public static void AppendSynDataInfo(SynDataInfoEntity entity)
        {
            ParamHelper.wcfSynData.Add(entity);
            // 新建一个对象，避免操作同一个对象引起的ID问题
            SynDataInfoEntity model = new SynDataInfoEntity();
            model.ID = entity.ID;
            model.AccountName = entity.AccountName;
            model.AccountPwd = entity.AccountPwd;
            model.Token = entity.Token;
            model.DataTag = entity.DataTag;
            model.DataType = entity.DataType;
            model.DealWithDate = entity.DealWithDate;
            model.DealWithId = entity.DealWithId;
            model.DealWithType = entity.DealWithType;

            DataHelper.AppendData(model, ParamHelper.SynDatas);
            ParamHelper.SynDataMax++;
        }

        public static void RemoveSynDataInfo(SynDataInfoEntity entity)
        {
            DataHelper.RemoveData(entity, ParamHelper.SynDatas);
            ParamHelper.wcfSynData.Delete(entity);
            ParamHelper.SynDataMax--;
        }

        public static void AppendSynCheckedDataInfo(SynCheckedDataInfoEntity entity)
        {
            ParamHelper.wcfSynCheckedData.Add(entity);
            // 新建一个对象，避免操作同一个对象引起的ID问题
            SynCheckedDataInfoEntity model = new SynCheckedDataInfoEntity();
            model.ID = entity.ID;
            model.AccountName = entity.AccountName;
            model.DataTag = entity.DataTag;
            model.DataType = entity.DataType;
            model.DealWithDate = entity.DealWithDate;
            model.DealWithId = entity.DealWithId;
            model.DealWithType = entity.DealWithType;
            model.FileName = entity.FileName;
            model.FilePath = entity.FilePath;
            model.IsDownLoad = entity.IsDownLoad;

            DataHelper.AppendData(model, ParamHelper.SynCheckedDatas);
            ParamHelper.SynCheckedDataMax++;
        }

        public static void RemoveSynCheckedDataInfo(SynCheckedDataInfoEntity entity)
        {
            DataHelper.RemoveData(entity, ParamHelper.SynCheckedDatas);
            ParamHelper.wcfSynCheckedData.Delete(entity);
            ParamHelper.SynCheckedDataMax--;
        }

        public static string SetDefault(string strValue)
        {
            return (string.IsNullOrWhiteSpace(strValue) || strValue == "-") ? "0" : strValue;
        }
    }
}