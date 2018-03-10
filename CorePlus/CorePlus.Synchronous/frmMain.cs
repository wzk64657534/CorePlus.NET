using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

using Core;
using System.Text;
using System.Reflection;
using CorePlus.Repository;

namespace CorePlus.Synchronous
{
    public partial class frmMain : Form
    {
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                ParamHelper.managerMaterail = new MaterialManager();
                ParamHelper.managerDealWith = new DealWithManager();
                ParamHelper.managerReport = new ReportManager();
                ParamHelper.managerRealTime = new RealTimeManager();

                ParamHelper.wcfAccount = new AccountRepository();
                ParamHelper.wcfCampaign = new CampaignRepository();
                ParamHelper.wcfAdgroup = new AdgroupRepository();
                ParamHelper.wcfKeyword = new KeywordRepository();
                ParamHelper.wcfCreative = new CreativeRepository();
                ParamHelper.wcfSublink = new SublinkRepository();
                ParamHelper.wcfFolder = new FolderRepository();
                ParamHelper.wcfMonitor = new MonitorRepository();

                ParamHelper.wcfAccountStatistics = new AccountStatisticsRepository();
                ParamHelper.wcfCampaignStatistics = new CampaignStatisticsRepository();
                ParamHelper.wcfAdgroupStatistics = new AdgroupStatisticsRepository();
                ParamHelper.wcfKeywordStatistics = new KeywordStatisticsRepository();
                ParamHelper.wcfCreativeStatistics = new CreativeStatisticsRepository();

                ParamHelper.wcfSynCheckedData = new SynCheckedDataRepository();
                ParamHelper.wcfSynData = new SynDataRepository();
                ParamHelper.wcfLog = new LogRepository();

                // 主程序启动后，先加载数据。如果有新的在尾部追加
                ParamHelper.SynDatas = null;
                ParamHelper.SynDatas = ParamHelper.wcfSynData.FindAll().ToList();
                ParamHelper.SynDataMax = ParamHelper.SynDatas.Count;
                // 主程序启动后，遍历数据请求数据
                ParamHelper.SynCheckedDatas = null;
                ParamHelper.SynCheckedDatas = ParamHelper.wcfSynCheckedData.FindAll().ToList();
                ParamHelper.SynCheckedDataMax = ParamHelper.SynCheckedDatas.Count;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, "Load_InitParam");
            }
        }

        private void Run()
        {
            try
            {
                while (true)
                {
                    if (!ParamHelper.IsRun()) { return; }

                    SynDataDirector director = new SynDataDirector();
                    // 获取所有继承基类的build类，然后运行它的实例
                    var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.Contains("Builder") && !x.Name.Contains("Base")).Select(x => x).ToList();
                    var baseType = typeof(BaseBuilder);
                    if (types != null)
                    {
                        foreach (var item in types)
                        {
                            var tmp = item.BaseType;
                            if (tmp == null) { continue; }
                            if (tmp == baseType)
                            {
                                BaseBuilder obj = CreateObject(item.FullName) as BaseBuilder;
                                if (obj != null)
                                {
                                    director.Build(obj);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, "Run");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Run":
                    btn.Text = "Stop";
                    this.Run();
                    LogHelper.AddLog("程序已经启动", "Run");
                    break;
                case "Stop":
                    this.btnRun.Text = "Run";
                    LogHelper.AddLog("程序已经终止", "Stop");
                    break;
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private object CreateObject(string typeName)
        {
            object obj = null;
            Type objType = Type.GetType(typeName, true);
            obj = Activator.CreateInstance(objType);
            return obj;
        }
    }
}