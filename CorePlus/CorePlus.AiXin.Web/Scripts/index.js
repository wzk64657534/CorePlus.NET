$(function () {
    $('#generator ul li').click(function () {
        var btn = $(this);
        var id = btn.attr('id');
        var json = '{';
        json = json + '"UserID":"00000",';
        json = json + '"Password":"501ae668de56c561f98b68b2c1c50e6b",';
        json = json + '"CompanyCode":"51900000",';
        json = json + '"ShopCode":"001",';
        json = json + '"PosCode":"005",';
        json = json + '"PosMark":"AC4FB26212D3F13997E7BF041556ECA8",';
        json = json + '"LoginSign":"F6A559C95E24E3BEA5AB40A525C46DA7501c4f7bbd3fb63a9ac3170e1e3ce846",';
        switch (id) {
            case 'LoginSign':
                json = json + '"ActionName":"LoginSign"';
                break;
            case 'MEMBER_CHECKCODE@DIMOOAPP': //2.	地猫会员检查帐号
                json = json + '"ActionName":"MEMBER_CHECKCODE@DIMOOAPP",';
                json = json + '"MemberCode":""';
                break;
            case 'MEMBER_REGISTER@DIMOOAPP': //3.	地猫会员注册帐号
                json = json + '"ActionName":"MEMBER_REGISTER@DIMOOAPP",';
                json = json + '"MemberCode":"",';
                json = json + '"LoginPassword":""';
                break;
            case 'MEMBER_LOGIN@DIMOOAPP': //4.	地猫会员登录帐号
                json = json + '"ActionName":"MEMBER_LOGIN@DIMOOAPP",';
                json = json + '"MemberCode":"",';
                json = json + '"LoginPassword":""';
                break;
            case 'MEMBER_CHANGEPASSWORD@DIMOOAPP': //5.	地猫会员修改密码
                json = json + '"ActionName":"MEMBER_CHANGEPASSWORD@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"MemberCode":"",';
                json = json + '"LoginPassword":"",';
                json = json + '"NewPassword":""';
                break;
            case 'MEMBER_CHANGEINFO@DIMOOAPP': //6.	地猫会员修改基础信息
                json = json + '"ActionName":"MEMBER_CHANGEINFO@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"MemberName":"",';
                json = json + '"NickName":"",';
                json = json + '"IdCardNum":"",';
                json = json + '"Birtyday":"",';
                json = json + '"Sex":"",';
                json = json + '"HomeAddress":"",';
                json = json + '"CompanyAddress":"",';
                json = json + '"TelePhone":"",';
                json = json + '"ProtectQuestion":"",';
                json = json + '"ProtectAnswer":"",';
                json = json + '"Remark":"",';
                json = json + '"MemberPoint":"",';
                json = json + '"MemberLevel":"",';
                json = json + '"UserLogo":"",';
                json = json + '"MinLogo":"",';
                json = json + '"MaxLogo":"",';
                json = json + '"DefaultLogo":"",';
                json = json + '"Status":"",';
                json = json + '"IsEnabled":""';
                break;
            case 'MEMBER_GETINFO@DIMOOAPP': //7.	地猫会员获取基础信息
                json = json + '"ActionName":"MEMBER_GETINFO@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}"';
                break;
            case 'MEMBER_BINDALLFULLCARD@DIMOOAPP': //8.	地猫会员绑定号付通
                json = json + '"ActionName":"MEMBER_BINDALLFULLCARD@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"AllFullCardID":"",';
                json = json + '"AllFullCardPassword":""';
                break;
            case 'MEMBER_UNBINDALLFULLCARD@DIMOOAPP': //9.	地猫会员解绑号付通
                json = json + '"ActionName":"MEMBER_UNBINDALLFULLCARD@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"AllFullCardID":""';
                break;
            case 'MEMBER_COLLECTPRODUCT@DIMOOAPP': //10.	地猫会员收藏商品
                json = json + '"ActionName":"MEMBER_COLLECTPRODUCT@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"ProductID":""';
                break;
            case 'MEMBER_CANCELCOLLECTPRODUCT@DIMOOAPP': //11.	地猫会员取消收藏
                json = json + '"ActionName":"MEMBER_CANCELCOLLECTPRODUCT@DIMOOAPP",';
                json = json + '"MemberID":"",';
                json = json + '"ProductID":""';
                break;
            case 'MEMBER_QUERYPRODUCTORDERLIST@DIMOOAPP': //12.	地猫会员商品订单列表查询
                json = json + '"ActionName":"MEMBER_QUERYPRODUCTORDERLIST@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"BillMonths":"0",';
                json = json + '"BillStatus":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_QUERYHOLDDMCARDLIST@DIMOOAPP': //13.	地猫会员我的电子卡列表查询
                json = json + '"ActionName":"MEMBER_QUERYHOLDDMCARDLIST@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"PubCompanyCode":"",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_QUERYCOLLECTLIST@DIMOOAPP': //14.	地猫会员我的收藏列表查询
                json = json + '"ActionName":"MEMBER_QUERYCOLLECTLIST@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_QUERYARTICLELIST@DIMOOAPP': //15.	地猫会员地猫公告
                json = json + '"ActionName":"MEMBER_QUERYARTICLELIST@DIMOOAPP",';
                json = json + '"ArticleClass":"",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'PRODUCT_INFOLIST@DIMOOAPP': //16.	预付费商品列表
                json = json + '"ActionName":"PRODUCT_INFOLIST@DIMOOAPP",';
                json = json + '"IndustryClassData":"",';
                json = json + '"AreaClassData":"",';
                json = json + '"PubCompanyCode":"",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"FaceValueMin":"",';
                json = json + '"FaceValueMax":"",';
                json = json + '"CanBeShared":"",';
                json = json + '"CanBeHandseled":"",';
                json = json + '"CanBeRansomed":"",';
                json = json + '"SortName":"1",';
                json = json + '"SortOrder":"1",';
                json = json + '"IsEnable":"1",';
                json = json + '"SearchText":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'PRODUCT_CITYLIST@DIMOOAPP': //17.	预付费商品区域分类
                json = json + '"ActionName":"PRODUCT_CITYLIST@DIMOOAPP",';
                json = json + '"IsEnable":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'PRODUCT_CLASSLIST@DIMOOAPP': //18.	预付费商品分类列表
                json = json + '"ActionName":"PRODUCT_CLASSLIST@DIMOOAPP",';
                json = json + '"CityCode":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'PRODUCT_NEWORDER@DIMOOAPP': //19.	预付费商品新建订单
                json = json + '"ActionName":"PRODUCT_NEWORDER@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"RecCount":"",';
                json = json + '"ProductID1":"",';
                json = json + '"BuyNum1":"",';
                json = json + '"ProductID2":"",';
                json = json + '"BuyNum2":"",';
                json = json + '"BuyNumn":""';
                break;
            case 'PRODUCT_PAYORDER@DIMOOAPP': //20.	预付费商品支付订单
                json = json + '"ActionName":"PRODUCT_PAYORDER@DIMOOAPP",';
                json = json + '"BillID":"",';
                json = json + '"PayMoney":"",';
                json = json + '"HandFee":"",';
                json = json + '"RecAccountID":"",';
                json = json + '"PayBankInfo":"",';
                json = json + '"PayAccountCode":"",';
                json = json + '"PaymentSerialCode":""';
                break;
            case 'PRODUCT_QUERYORDER@DIMOOAPP': //21.	预付费商品订单查询
                json = json + '"ActionName":"PRODUCT_QUERYORDER@DIMOOAPP",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"BillID":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'PRODUCT_ORDERLINE@DIMOOAPP': //22.	预付费商品订单明细
                json = json + '"ActionName":"PRODUCT_ORDERLINE@DIMOOAPP",';
                json = json + '"AllFullCardID":"",';
                json = json + '"PubCompanyCode":"",';
                json = json + '"ProductID":"",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"BeginTime":"",';
                json = json + '"EndTime":"",';
                json = json + '"IsInvoiced":"",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_QUERYSHAREOUTMSGLIST@DIMOOAPP': //23.	猫市转卖信息列表（转卖搜搜）
                json = json + '"ActionName":"MEMBER_QUERYSHAREOUTMSGLIST@DIMOOAPP",';
                json = json + '"CityCode":"",';
                json = json + '"IndustryClassData":"",';
                json = json + '"AreaClassData":"",';
                json = json + '"PubCompanyCode":"",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"FaceValueMin":"",';
                json = json + '"FaceValueMax":"",';
                json = json + '"SortName":"0",';
                json = json + '"SortOrder":"1",';
                json = json + '"IsOver":"1",';
                json = json + '"SearchText":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_QUERYSHAREOUTPUBCOMPANYLIST@DIMOOAPP': //24.猫市转卖推荐商户
                json = json + '"ActionName":"MEMBER_QUERYSHAREOUTPUBCOMPANYLIST@DIMOOAPP",';
                json = json + '"CityCode":"",';
                json = json + '"IndustryClassData":"",';
                json = json + '"AreaClassData":"",';
                json = json + '"FaceValueMin":"",';
                json = json + '"FaceValueMax":"",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"SearchText":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_PUBLISHSHAREOUTMSG@DIMOOAPP': //25.	猫市发布转卖信息
                json = json + '"ActionName":"MEMBER_PUBLISHSHAREOUTMSG@DIMOOAPP",';
                json = json + '"CityCode":"",';
                json = json + '"MessageID":"",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"NickName":"",';
                json = json + '"TelePhone":"",';
                json = json + '"DmCardID":"",';
                json = json + '"ShareTitle":"",';
                json = json + '"ShareFaceValue":"",';
                json = json + '"SharePriceValue":"",';
                json = json + '"ShareNumber":""';
                break;
            case 'MEMBER_GETONESHAREOUTMSGINFO@DIMOOAPP': //26.	猫市查询单条转卖发布信息
                json = json + '"ActionName":"MEMBER_GETONESHAREOUTMSGINFO@DIMOOAPP",';
                json = json + '"MessageID":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_QUERYSHAREINMSGLIST@DIMOOAPP': //27.	猫市求购信息列表（求购搜搜）
                json = json + '"ActionName":"MEMBER_QUERYSHAREINMSGLIST@DIMOOAPP",';
                json = json + '"CityCode":"",';
                json = json + '"IndustryClassData":"",';
                json = json + '"AreaClassData":"",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"QueryPageSize":"20",';
                json = json + '"PageIndex":"1",';
                json = json + '"FaceValueMin":"",';
                json = json + '"FaceValueMax":"",';
                json = json + '"SortName":"0",';
                json = json + '"SortOrder":"1",';
                json = json + '"IsOver":"1",';
                json = json + '"SearchText":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'MEMBER_PUBLISHSHAREINMSG@DIMOOAPP': //28.	猫市发布求购信息
                json = json + '"ActionName":"MEMBER_PUBLISHSHAREINMSG@DIMOOAPP",';
                json = json + '"IndustryClassID":"",';
                json = json + '"CityCode":"",';
                json = json + '"AreaClassID":"",';
                json = json + '"MessageID":"",';
                json = json + '"MemberID":"{90918D07-18AD-4B7E-AD59-2E8F7FFB6887}",';
                json = json + '"NickName":"",';
                json = json + '"TelePhone":"",';
                json = json + '"MessageTitle":"",';
                json = json + '"MessageContent":"",';
                json = json + '"FaceValue":"",';
                json = json + '"PriceValue":"",';
                json = json + '"ShareNumber":""';
                break;
            case 'MEMBER_GETONESHAREINMSGINFO@DIMOOAPP': //29.	猫市查询单条求购发布信息
                json = json + '"ActionName":"MEMBER_GETONESHAREINMSGINFO@DIMOOAPP",';
                json = json + '"MessageID":"",';
                json = json + '"DataMode":"DEFXML"';
                break;
            case 'PRODUCT_GETONEINFO@DIMOOAPP': //30.	猫市商品基础信息
                json = json + '"ActionName":"PRODUCT_GETONEINFO@DIMOOAPP",';
                json = json + '"ProductID":"",';
                json = json + '"DataMode":"DEFXML"';
                break;

            default: break;
        }
        json = json + '}';
        $('#body').val(json);
    });
});