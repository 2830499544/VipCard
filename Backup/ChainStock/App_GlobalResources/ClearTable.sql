--Mem
drop table Mem
CREATE TABLE [Mem]([MemID] [int] IDENTITY(1,1) NOT NULL,[MemCard] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemPassword] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemSex] [bit] NULL,[MemIdentityCard] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemMobile] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemPhoto] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,[MemBirthdayType] [bit] NULL,[MemBirthday] [datetime] NULL,[MemIsPast] [bit] NULL,[MemPastTime] [datetime] NULL,[MemPoint] [int] NULL CONSTRAINT [DF_Mem_MemPoint]  DEFAULT ((0)),[MemPointAutomatic] [bit] NULL,[MemMoney] [money] NULL CONSTRAINT [DF_Mem_MemMoney]  DEFAULT ((0)),[MemConsumeMoney] [money] NULL,[MemConsumeLastTime] [datetime] NULL,[MemConsumeCount] [int] NULL,[MemEmail] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemAddress] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,[MemState] [int] NULL,[MemRecommendID] [int] NULL,[MemLevelID] [int] NULL,[MemShopID] [int] NULL,[MemCreateTime] [datetime] NULL,[MemRemark] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,[MemUserID] [int] NULL,[MemTelePhone] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemQRCode] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,[MemProvince] [varchar](50) NULL,[MemCity] [varchar](50) NULL,[MemCounty] [varchar](50) NULL,[MemVillage] [varchar](50) NULL,[MemQuestion] [varchar](500) NULL,[MemAnswer] [varchar](500) NULL,[MemWeiXinCard] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemCardNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemAttention] [int] NULL,[MemWeiXinCards] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([MemID] ASC)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY] 
SET IDENTITY_Insert Mem ON
insert into Mem(MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemCardNumber,MemAttention,MemWeiXinCards) values (0,0,'E62A9E6C1892C6BB','散客',1,NULL,NULL,NULL,NULL,'1900-1-1 0:00:00',0,'2900-1-1 0:00:00',0,NULL,0,NULL,NULL,NULL,NULL,NULL,0,NULL,-1,1,'2013-4-20',NULL,1,NULL,'','','','','','','','','','') 
dbcc checkident(Mem,reseed,0)
SET IDENTITY_Insert Mem OFF
--Goods
drop table Goods
CREATE TABLE [Goods]([GoodsID] [int] IDENTITY(1,1) NOT NULL,[GoodsCode] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[GoodsClassID] [int] NULL,[Name] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,[NameCode] [varchar](20) COLLATE Chinese_PRC_CI_AS NULL,[Unit] [nvarchar](10) COLLATE Chinese_PRC_CI_AS NULL,[GoodsNumber] [int] NULL,[SalePercet] [real] NULL,[GoodsSaleNumber] [int] NULL,[Price] [money] NULL,[CommissionType] [int] NULL,[CommissionNumber] [float] NULL,[Point] [int] NULL,[MinPercent] [real] NULL,[GoodsType] [tinyint] NULL,[GoodsBidPrice] [money] NULL,[GoodsRemark] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,[GoodsPicture] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[GoodsCreateTime] [datetime] NULL,[CreateShopID] [int] NULL,CONSTRAINT [PK_Goods] PRIMARY KEY CLUSTERED ([GoodsID] ASC)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
--Coupon
DELETE Coupon DBCC CHECKIDENT(Coupon,RESEED,0)
--CouponList
DELETE CouponList DBCC CHECKIDENT(CouponList,RESEED,0)
--GoodsAllot
DELETE GoodsAllot DBCC CHECKIDENT(GoodsAllot,RESEED,0)
--GoodsAllotDetail
DELETE GoodsAllotDetail DBCC CHECKIDENT(GoodsAllotDetail,RESEED,0)
--GoodsClass
DELETE GoodsClass DBCC CHECKIDENT(GoodsClass,RESEED,0)
SET IDENTITY_Insert GoodsClass ON
insert into GoodsClass(ClassID,ClassName,ClassRemark,ParentID,CreateShopID) values (1,'基本类别','基本类别',0,1)
SET IDENTITY_Insert GoodsClass OFF
--GoodsClassAuthority
DELETE GoodsClassAuthority DBCC CHECKIDENT(GoodsClassAuthority,RESEED,0)
SET IDENTITY_Insert GoodsClassAuthority ON
insert into GoodsClassAuthority(ClassAuthorityID,ClassID,ShopID) values (1,1,1)
SET IDENTITY_Insert GoodsClassAuthority OFF
--GoodsLog
DELETE GoodsLog DBCC CHECKIDENT(GoodsLog,RESEED,0)
--GoodsLogDetail
DELETE GoodsLogDetail DBCC CHECKIDENT(GoodsLogDetail,RESEED,0)
--GoodsNumber
DELETE GoodsNumber DBCC CHECKIDENT(GoodsNumber,RESEED,0)
--MemCount
DELETE MemCount DBCC CHECKIDENT(MemCount,RESEED,0)
--MemCountDetail
DELETE MemCountDetail DBCC CHECKIDENT(MemCountDetail,RESEED,0)
--MemCustomField
DELETE MemCustomField DBCC CHECKIDENT(MemCustomField,RESEED,1)
--MemDrawMoney
DELETE MemDrawMoney DBCC CHECKIDENT(MemDrawMoney,RESEED,0)
--MemLevel
DELETE MemLevel where LevelID>0  DBCC CHECKIDENT(MemLevel,RESEED,0)
--SysShopMemLevel
DELETE SysShopMemLevel where ShopMemLevelID>0  DBCC CHECKIDENT(SysShopMemLevel,RESEED,0)
--MemRecharge
DELETE MemRecharge DBCC CHECKIDENT(MemRecharge,RESEED,0)
--OrderDetail
DELETE OrderDetail DBCC CHECKIDENT(OrderDetail,RESEED,0)
--OrderLog
DELETE OrderLog DBCC CHECKIDENT(OrderLog,RESEED,0)
--PointExchange
DELETE PointExchange DBCC CHECKIDENT(PointExchange,RESEED,0)
--PointGift
DELETE PointGift DBCC CHECKIDENT(PointGift,RESEED,0)
--PointLog
DELETE PointLog DBCC CHECKIDENT(PointLog,RESEED,0)
--ScreenPopUp
DELETE ScreenPopUp DBCC CHECKIDENT(ScreenPopUp,RESEED,0)
--SmsLog
DELETE SmsLog DBCC CHECKIDENT(SmsLog,RESEED,0)
--SmsTemplate
DELETE SmsTemplate where TemplateID>11  DBCC CHECKIDENT(SmsTemplate,RESEED,11)
--Staff
DELETE Staff DBCC CHECKIDENT(Staff,RESEED,1)
--StaffClass
DELETE StaffClass DBCC CHECKIDENT(StaffClass,RESEED,1)
--StaffMoney
DELETE StaffMoney DBCC CHECKIDENT(StaffMoney,RESEED,0)
--SysCustomRemind
DELETE SysCustomRemind DBCC CHECKIDENT(SysCustomRemind,RESEED,0)
--SysDbBackUp
DELETE SysDbBackUp DBCC CHECKIDENT(SysDbBackUp,RESEED,0)
--SysLog
DELETE SysLog DBCC CHECKIDENT(SysLog,RESEED,0)
--SysNotice
DELETE SysNotice DBCC CHECKIDENT(SysNotice,RESEED,0)
--SysShopAuthority
DELETE SysShopAuthority where ShopAuthorityID>1 DBCC CHECKIDENT(SysShopAuthority,RESEED,1)
UPDATE SysShopAuthority SET ShopAuthorityData = '1' WHERE ShopAuthorityID = 1
--SysUser
DELETE SysUser where UserID>1  DBCC CHECKIDENT(SysUser,RESEED,1)
--OrderTime
DELETE OrderTime  DBCC CHECKIDENT(OrderTime,RESEED,0)
--GiftClass
DELETE GiftClass DBCC CHECKIDENT(GiftClass,RESEED,0)
--Message
DELETE Message DBCC CHECKIDENT(Message,RESEED,0)
--GiftExchange
DELETE GiftExchange DBCC CHECKIDENT(GiftExchange,RESEED,0)
--GiftExchangeDetail
DELETE GiftExchangeDetail DBCC CHECKIDENT(GiftExchangeDetail,RESEED,0)
--SysUserWork
DELETE SysUserWork DBCC CHECKIDENT(SysUserWork,RESEED,0)
--WeiXinRule
DELETE WeiXinRule DBCC CHECKIDENT(WeiXinRule,RESEED,1) 
--WeiXinNews
DELETE WeiXinNews DBCC CHECKIDENT(WeiXinNews,RESEED,0)
--WeiXinLog
DELETE WeiXinLog DBCC CHECKIDENT(WeiXinNews,RESEED,0)
--GoodsClassDiscount
DELETE GoodsClassDiscount DBCC CHECKIDENT(GoodsClassDiscount,RESEED,0)
SET IDENTITY_Insert GoodsClassDiscount ON
insert into GoodsClassDiscount(ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) values (1,1,0,1,1.00,1.00)
SET IDENTITY_Insert GoodsClassDiscount OFF
--SysError
DELETE SysError DBCC CHECKIDENT(SysError,RESEED,0)
--SysParameter
UPDATE SysParameter SET SmsSeries = '' ,SmsSerialPwd = '',Sms = '0',MoneySms = '0',AutoPrint='0',AccordPrint='0',PrintTitle='',PrintFootNote='',IsAutoSendSMSByMemPast='0',AutoSendSMSByMemPastForDay=0,IsAutoSendSMSByMemBirthday='0',AutoSendSMSByMemBirthdayForDay=0,IsStartWeiXin='0',IsStartTimingProject='0',IsStartMemCount='0',MarketingSMS='0',MarketingSmsSeries='',MarketingSmsSerialPwd='',IsEmail='0',IsEmailNotice='0',EmailAdress='',EmailPwd='',EmailSMTP='',EnterpriseEmailDisplayName='',SellerAccount='',PartnerID='',PartnerKey=''
--MoneyChangeLog
DELETE MoneyChangeLog DBCC CHECKIDENT(MoneyChangeLog,RESEED,0)
--Timingrules
DELETE Timingrules DBCC CHECKIDENT(Timingrules,RESEED,0)
--TimingProject
DELETE TimingProject DBCC CHECKIDENT(TimingProject,RESEED,0)
--TimingCategory
DELETE TimingCategory DBCC CHECKIDENT(TimingCategory,RESEED,0)
--MemStorageTiming
DELETE MemStorageTiming DBCC CHECKIDENT(MemStorageTiming,RESEED,0)
--SysGroupAuthority
DELETE SysGroupAuthority where GroupID>3
--SysGroup
DELETE SysGroup where GroupID>3  DBCC CHECKIDENT(SysGroup,RESEED,3)
--EmailLog
DELETE EmailLog DBCC CHECKIDENT(EmailLog,RESEED,0)  
--Proposal 会员反馈
DELETE Proposal DBCC CHECKIDENT(Proposal,RESEED,0)
--ProductCenter 产品中心
DELETE ProductCenter DBCC CHECKIDENT(ProductCenter,RESEED,0)
--SymbolShow 形象展示
DELETE SymbolShow DBCC CHECKIDENT(SymbolShow,RESEED,0)
--Promotions 优惠活动
DELETE Promotions DBCC CHECKIDENT(Promotions,RESEED,0)
--MemberExplanation 商家公布
DELETE MemberExplanation DBCC CHECKIDENT(MemberExplanation,RESEED,0)
--MicroWebsiteGiftExchange 兑换审核 兑换统计
DELETE MicroWebsiteGiftExchange DBCC CHECKIDENT(MicroWebsiteGiftExchange,RESEED,0)
--MicroWebsiteGiftExchangeDetail
DELETE MicroWebsiteGiftExchangeDetail DBCC CHECKIDENT(MicroWebsiteGiftExchangeDetail,RESEED,0)
--MicroWebsiteGoodsClass 商品分类
DELETE MicroWebsiteGoodsClass DBCC CHECKIDENT(MicroWebsiteGoodsClass,RESEED,0)
--MicroWebsiteGoods 商品列表
DELETE MicroWebsiteGoods DBCC CHECKIDENT(MicroWebsiteGoods,RESEED,0)
--MicroWebsiteOrderLog 消费记录 消费审核
DELETE MicroWebsiteOrderLog DBCC CHECKIDENT(MicroWebsiteOrderLog,RESEED,0)
--MicroWebsiteOrderLogDetail
DELETE MicroWebsiteOrderLogDetail DBCC CHECKIDENT(MicroWebsiteOrderLogDetail,RESEED,0)
--WeiXinMenu
DELETE WeiXinMenu where MenuID>7 DBCC CHECKIDENT(WeiXinMenu,RESEED,7)
--SysShopBuyCard 商家购卡
DELETE SysShopBuyCard DBCC CHECKIDENT(SysShopBuyCard,RESEED,0)
--SysShopSettlement 商家结算
DELETE SysShopSettlement DBCC CHECKIDENT(SysShopSettlement,RESEED,0)
--商家积分结算
DELETE  SysShopPointSettlement DBCC CHECKIDENT(SysShopPointSettlement,RESEED,0)
--SysShopPointLog 商家积分变动记录
DELETE SysShopPointLog DBCC CHECKIDENT(SysShopPointLog,RESEED,0)
--SysShopCmsLog  商家短信变动记录
DELETE SysShopCmsLog DBCC CHECKIDENT(SysShopCmsLog,RESEED,0)
--SysShop 商家
DELETE SysShop where ShopID>1  DBCC CHECKIDENT(SysShop,RESEED,1)
update SysShop set PointCount=0,SmsCount=0
--pointdraw  商家短信变动记录
DELETE pointdraw DBCC CHECKIDENT(pointdraw,RESEED,0)
--ReturnPointLog  商家短信变动记录
DELETE ReturnPointLog DBCC CHECKIDENT(ReturnPointLog,RESEED,0)
--OnlineMessage  商家短信变动记录
DELETE OnlineMessage DBCC CHECKIDENT(OnlineMessage,RESEED,0)
--MemTransferLog  商家短信变动记录
DELETE MemTransferLog DBCC CHECKIDENT(MemTransferLog,RESEED,0)








