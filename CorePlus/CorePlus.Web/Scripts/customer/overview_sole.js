var page=new T.Webpage({components:["Indicator","Tip",{type:"Download",options:{reportTypes:["pdf"]}},{id:"IndexLine",type:"Flash",options:{flashContainerId:"FlashLineContainer",height:230,type:"line"}},"ShortDate","SimpleFlashIndicator",{id:"DateCompare",type:"DateCompare",options:{checkedId:"LastDay"}},"FlashFormatConverter"],methods:{getCommonTrackRpt:{name:"getCommonTrackRpt",uri:"overview/getCommonTrackRpt"},getTimeTrendRpt:{name:"getTimeTrendRpt",uri:"overview/getTimeTrendRpt",adapter:"convertFlashLineDataFormat"},getDistrictRpt:{name:"getDistrictRpt",uri:"overview/getDistrictRpt",adapter:"convertFlashAreaDataFormat"},getChartData:{name:"getChartData",uri:"overview/getDistrictRpt",adapter:"convertFlashAreaDataFormat"},modifyTrade:{name:"modifyTrade",uri:"home/site/modifyTrade"},saveChart:{name:"saveChart",uri:"home/chart/save"}},onbeforeInit:function(){this.context.siteId=this.siteInfo.id;this.context.st=this.pageInfo.st;this.context.et=this.pageInfo.et;this.context.st2=this.pageInfo.st2;this.context.et2=this.pageInfo.et2;this.context.indicators=this.indexInfo.flashSelected.join();this.context.sex=this.sex;this.context.age=this.age;T.dom.empty("VisitorAttr");T.insertHTML("VisitorAttr","beforeEnd",AceTemplate.format("VisitorAttrTemplate",{sex:this.sex,age:this.age}));if(this.pageInfo.hasNewFunctionData){this.components.push({id:"Guide",type:"Guide",options:{newFunctionGuide:true}})}var b=this;if(this.pageInfo.showTradeSetting){this.context.tradeDialog=new T.ui.Dialog({titleText:"",content:AceTemplate.format("TradeTemplate",{items:null}),isModal:false,className:"trade-dialog",zIndex:5554,height:200});this.context.tradeDialog.show();T.dom.setStyles(this.context.tradeDialog.getContainer(),{left:"auto",right:20,top:20});var c=new T.ui.SelectGroup({containerId:"TradeSelectContainer",label:"行业类别&nbsp;&nbsp;",autoRender:true,items:T.config.tradeList,onchange:function(d){b.context.trade=d}});T.on("TradeSettingSave","click",function(d){b.modifyTrade()});T.on("TradeHidden","click",function(d){var e={siteId:b.context.siteId,pageId:0,elementId:"",type:"pop",status:this.checked?0:1};T.sio.log(T.config.systemConfig.memoUri+"?"+T.param(e))})}if(T.ie<7){var a={id:"IndexArea",type:"Flash",options:{type:"area",flashContainerId:"ChinaArea"}};this.components[this.components.length]=a}},oninit:function(){var a=this;this.context.st2=this.DateCompare.params.st2;this.context.et2=this.DateCompare.params.et2;this.context.outline=T.object.clone(this.outline);this.context.outline.fields=this.Indicator.map(this.outline.fields);this.context.outline.fields[0]=T.array.find(this.indexInfo.indexes,function(b){return a.outline.fields[0].toString()==b.id});T.dom.empty("IndexPreview");T.insertHTML("IndexPreview","beforeEnd",AceTemplate.format("IndexPreviewTableListTemplate",this.context.outline));this.context.isComparable=this.DateCompare.isComparable;if(Raphael.svg||T.ie>=7){this.getChartData()}this.getCommonTrackData()},context:{siteId:null,sex:null,age:null,outline:null,line:"IndexLine",area:"IndexArea",chartContainer:"ChinaArea",flashIndicators:null,indicators:null,fields:null,st:null,et:null,st2:null,et2:null,reportType:null,isComparable:0,trade:null,tradeDialog:null},getParams:function(){return{st:this.context.st,et:this.context.et,st2:this.context.st2,et2:this.context.et2,indicators:this.context.indicators,siteId:this.context.siteId}},getDistrictParams:function(){var a=this.getParams();a.indicators="pv_count";return a},onchangeToggleTarget:function(a){if(T.dom.hasClass(a,"close")){T.dom.addClass(T.dom.getParent(a),"btw")}else{T.dom.removeClass(T.dom.getParent(a),"btw")}},onchangeDateCompare:function(a){this.context.st2=a.st2;this.context.et2=a.et2;this.getFlashTrendData()},onchangeSimpleFlashIndicator:function(a){this.context.indicators=a.join();this.getFlashTrendData()},onchangeShortDate:function(a){this.context.st=a[0];this.context.et=a[1];if(a[0]==a[1]){this.DateCompare.show(parseInt(a[0]))}else{this.DateCompare.hide()}this.context.st2=this.DateCompare.params.st2;this.context.et2=this.DateCompare.params.et2;this.getFlashData();this.getCommonTrackData()},ongetCommonTrackRpt:function(){T.dom.empty("IndexSite");T.insertHTML("IndexSite","beforeEnd",AceTemplate.format("IndexSiteTableTemplate"))},ongetCommonTrackRptSuccess:function(b,a){T.dom.empty("IndexSite");T.insertHTML("IndexSite","beforeEnd",AceTemplate.format("IndexSiteTableTemplate",b))},ongetTimeTrendRpt:function(){this.IndexLine.showLoading&&this.IndexLine.showLoading()},ongetTimeTrendRptSuccess:function(b,a){this.refreshFlash(this.context.line,b,a)},ongetDistrictRpt:function(){},ongetDistrictRptSuccess:function(a){this.refreshFlash(this.context.area,a,status)},getChartData:function(){this.ajax(this.methods.getChartData,this.getDistrictParams())},ongetChartData:function(){T.dom.empty(this.context.chartContainer);T.dom.insertHTML(this.context.chartContainer,"beforeEnd",'<div class="loading">数据正在加载中...</div>')},ongetChartDataSuccess:function(a){this._data=a;if(!a){return}T.dom.empty(this.context.chartContainer);this.chinaAreaChart=new T.charts.ChinaArea(a,{containerId:this.context.chartContainer})},refreshFlash:function(c,d,b){this.FlashFormatConverter.setFlash(d);var a=this[c];if(this.context.st2||this.context.et2){a.setIsPeriodCompare&&a.setIsPeriodCompare(true)}else{a.setIsPeriodCompare&&a.setIsPeriodCompare(false)}},onconvertFlashLineDataFormat:function(a){a.by=this.context.st==this.context.et?"hour":"day";a.indicator=this.context.indicators;a.flashId=this.context.line;a.type="line"},onconvertFlashAreaDataFormat:function(a){a.indicator="pv_count";a.flashId=this.context.area;a.type="area";a.containerId=this.context.chartContainer},onflashLoaded:function(a){switch(a){case this.context.line:this.getFlashTrendData();break;case this.context.area:this.getFlashDistrictRptData();break;default:break}},getFlashData:function(){this.getFlashTrendData();if(T.ie<7){this.getFlashDistrictRptData()}else{this.getChartData()}},getCommonTrackData:function(){this.ajax(this.methods.getCommonTrackRpt,this.getParams())},getFlashDistrictRptData:function(){this.ajax(this.methods.getDistrictRpt,this.getDistrictParams())},getFlashTrendData:function(){this.ajax(this.methods.getTimeTrendRpt,this.getParams())},getDownloadParam:function(){return{st:this.context.st,et:this.context.et,st2:this.context.st2,et2:this.context.et2,flashIndicators:this.context.indicators,siteId:this.context.siteId}},onchangeDownload:function(){this.flashImages=[];this.sendFlashImage(this.IndexLine)},onflashCreateImageSuccess:function(a){this.flashImages.push(a);if(T.ie<7){switch(this.flashImages.length){case 1:this.sendFlashImage(this.IndexArea);break;case 2:var b=this.getDownloadParam();b.img=this.flashImages.join(",");this.Download.download(b);break;default:break}}else{if(this._data){this.saveChart()}else{var b=this.getDownloadParam();b.img=this.flashImages.join();this.Download.download(b)}}},saveChart:function(){var d=this.getDownloadParam();var b=[];var a=T.dom.create("div",{style:"width:365px;height:320px;position:absolute;left:-99999px;top:-99999px;","class":"export-chart-div"});document.body.appendChild(a);var c=new T.charts.ChinaArea(this._data,{animate:false,exportSVG:true,containerId:a});c.paper.setSize("365","300");b.push(c.paper.toSVG());d.charts=T.json.stringify(b);this.ajax(this.methods.saveChart,d)},onsaveChartSuccess:function(a){T.each(T.dom.q("export-chart-div"),function(c){T.dom.remove(c)});var b=this.getDownloadParam();b.img=this.flashImages.concat(a.imageName.split(",")).join();this.Download.download(b)},getModifyTradeParams:function(a){return{firstTradeId:this.context.trade[0],secondTradeId:this.context.trade[1],siteId:this.context.siteId}},modifyTrade:function(){if(this.context.trade){this.ajax(this.methods.modifyTrade,this.getModifyTradeParams())}},onmodifyTradeSuccess:function(){this.context.tradeDialog.close()}});