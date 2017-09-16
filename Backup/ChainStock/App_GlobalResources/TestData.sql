--Mem
drop table Mem
CREATE TABLE [Mem]([MemID] [int] IDENTITY(1,1) NOT NULL,[MemCard] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemPassword] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemSex] [bit] NULL,[MemIdentityCard] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemMobile] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemPhoto] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,[MemBirthdayType] [bit] NULL,[MemBirthday] [datetime] NULL,[MemIsPast] [bit] NULL,[MemPastTime] [datetime] NULL,[MemPoint] [int] NULL CONSTRAINT [DF_Mem_MemPoint]  DEFAULT ((0)),[MemPointAutomatic] [bit] NULL,[MemMoney] [money] NULL CONSTRAINT [DF_Mem_MemMoney]  DEFAULT ((0)),[MemConsumeMoney] [money] NULL,[MemConsumeLastTime] [datetime] NULL,[MemConsumeCount] [int] NULL,[MemEmail] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemAddress] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,[MemState] [int] NULL,[MemRecommendID] [int] NULL,[MemLevelID] [int] NULL,[MemShopID] [int] NULL,[MemCreateTime] [datetime] NULL,[MemRemark] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,[MemUserID] [int] NULL,[MemTelePhone] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemQRCode] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,[MemProvince] [varchar](50) NULL,[MemCity] [varchar](50) NULL,[MemCounty] [varchar](50) NULL,[MemVillage] [varchar](50) NULL,[MemQuestion] [varchar](500) NULL,[MemAnswer] [varchar](500) NULL,[MemWeiXinCard] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemCardNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[MemAttention] [int] NULL,[MemWeiXinCards] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([MemID] ASC)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY] 
SET IDENTITY_Insert Mem ON
insert into Mem(MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemCardNumber,MemAttention,MemWeiXinCards) values (0,0,'E62A9E6C1892C6BB','ɢ��',1,NULL,NULL,NULL,NULL,'1900-1-1 0:00:00',0,'2900-1-1 0:00:00',0,NULL,0,NULL,NULL,NULL,NULL,NULL,0,NULL,-1,1,'2013-4-20',NULL,1,NULL,'','','','','','','','','2','') 
insert into Mem(MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemCardNumber,MemAttention,MemWeiXinCards) values (1,'100010','2CBE9C73F856E743','�ܽ���',1,'','','',1,'1900-1-1 0:00:00',0,'2900-1-1 0:00:00',0,1,0.0000,NULL,NULL,NULL,'','',0,0,0,1,'2013-5-3 0:00:00','' ,1,'','','','','','','','','','2','') 
insert into Mem(MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemCardNumber,MemAttention,MemWeiXinCards) values (2,'100020','2CBE9C73F856E743','������',1,'','','',1,'1900-1-1 0:00:00',0,'2900-1-1 0:00:00',0,1,0.0000,NULL,NULL,NULL,'','',0,0,0,1,'2013-5-3 0:00:00','',1,'','','','','','','','','','2','') 
insert into Mem(MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemCardNumber,MemAttention,MemWeiXinCards) values (3,'100030','2CBE9C73F856E743','����',0,'','','',1,'1900-1-1 0:00:00',0,'2900-1-1 0:00:00',0,1,0.0000,NULL,NULL,NULL,'','',0,0,0,1,'2013-5-3 0:00:00','',1,'','','','','','','','','','2','') 
dbcc checkident(Mem,reseed,3)
SET IDENTITY_Insert Mem OFF
--GoodsClass
DELETE GoodsClass DBCC CHECKIDENT(GoodsClass,RESEED,0)
SET IDENTITY_Insert GoodsClass ON
insert into GoodsClass(ClassID,ClassName,ClassRemark,ParentID,CreateShopID) values (1,'�������','�������',0,1)
insert into GoodsClass(ClassID,ClassName,ClassRemark,ParentID,CreateShopID) values (2,'����ʳƷ','����С������',1,1)
insert into GoodsClass(ClassID,ClassName,ClassRemark,ParentID,CreateShopID) values (3,'�������','����С������',1,1)
insert into GoodsClass(ClassID,ClassName,ClassRemark,ParentID,CreateShopID) values (4,'�ǹ�','����С������',1,1)
SET IDENTITY_Insert GoodsClass OFF
--GoodsClassAuthority
DELETE GoodsClassAuthority DBCC CHECKIDENT(GoodsClassAuthority,RESEED,0)
SET IDENTITY_Insert GoodsClassAuthority ON
insert into GoodsClassAuthority(ClassAuthorityID,ClassID,ShopID) values (1,1,1)
insert into GoodsClassAuthority(ClassAuthorityID,ClassID,ShopID) values (2,2,1)
insert into GoodsClassAuthority(ClassAuthorityID,ClassID,ShopID) values (3,3,1)
insert into GoodsClassAuthority(ClassAuthorityID,ClassID,ShopID) values (4,4,1)
SET IDENTITY_Insert GoodsClassAuthority OFF
--Goods
drop table Goods
CREATE TABLE [Goods]([GoodsID] [int] IDENTITY(1,1) NOT NULL,[GoodsCode] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,[GoodsClassID] [int] NULL,[Name] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,[NameCode] [varchar](20) COLLATE Chinese_PRC_CI_AS NULL,[Unit] [nvarchar](10) COLLATE Chinese_PRC_CI_AS NULL,[GoodsNumber] [int] NULL,[GoodsSaleNumber] [int] NULL,[Price] [money] NULL,[CommissionType] [int] NULL,[CommissionNumber] [float] NULL,[SalePercet] [real] NULL,[Point] [int] NULL,[MinPercent] [real] NULL,[GoodsType] [tinyint] NULL,[GoodsBidPrice] [money] NULL,[GoodsRemark] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,[GoodsPicture] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,[GoodsCreateTime] [datetime] NULL,[CreateShopID] [int] NULL,CONSTRAINT [PK_Goods] PRIMARY KEY CLUSTERED ([GoodsID] ASC)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
SET IDENTITY_Insert Goods ON
insert into Goods(GoodsID,GoodsCode,GoodsClassID,[Name],NameCode,Unit,GoodsNumber,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID,SalePercet) values (1,'130503163627',2,'�Ϻü�','SHJ','��',0,0,5.00,0,0,-1,0,0,3.00,'','','2013-05-03 16:41:03.077',1,0)
insert into Goods(GoodsID,GoodsCode,GoodsClassID,[Name],NameCode,Unit,GoodsNumber,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID,SalePercet) values (2,'130503165141',3,'���ı���','KFALAZFSGJX','��',0,0,35.00,0,0,-1,0,0,10,'','','2013-05-03 16:53:26.720',1,0)
SET IDENTITY_Insert Goods OFF
--GoodsNumber
DELETE GoodsNumber DBCC CHECKIDENT(GoodsNumber,RESEED,0)
SET IDENTITY_Insert GoodsNumber ON
insert into GoodsNumber([ID],GoodsID,ShopID,Number) values(1,1,1,50)
insert into GoodsNumber([ID],GoodsID,ShopID,Number) values(4,2,1,50)
SET IDENTITY_Insert GoodsNumber OFF
--GoodsLog
DELETE GoodsLog DBCC CHECKIDENT(GoodsLog,RESEED,0)
--GoodsLogDetail
DELETE GoodsLogDetail DBCC CHECKIDENT(GoodsLogDetail,RESEED,0)
--Coupon
DELETE Coupon DBCC CHECKIDENT(Coupon,RESEED,0)
--CouponList
DELETE CouponList DBCC CHECKIDENT(CouponList,RESEED,0)
--GoodsAllot
DELETE GoodsAllot DBCC CHECKIDENT(GoodsAllot,RESEED,0)
--GoodsAllotDetail
DELETE GoodsAllotDetail DBCC CHECKIDENT(GoodsAllotDetail,RESEED,0)
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
--GiftClass
DELETE GiftClass DBCC CHECKIDENT(GiftClass,RESEED,0)
SET IDENTITY_Insert GiftClass ON
insert into GiftClass(GiftClassID,GiftClassName,GiftClassRemark,GiftParentID) values (1,'�մ�','',0)
SET IDENTITY_Insert GiftClass OFF
--PointGift
DELETE PointGift DBCC CHECKIDENT(PointGift,RESEED,0)
SET IDENTITY_Insert PointGift ON
insert into PointGift (GiftID,GiftName,GiftCode,GiftClassID,GiftPhoto,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber,GiftShopID,GiftRemark) values (1,'���ϱ�+�������','CYB+WXSB',1,'',5000,5,0,1,'')
insert into PointGift (GiftID,GiftName,GiftCode,GiftClassID,GiftPhoto,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber,GiftShopID,GiftRemark) values (2,'�մɱ�+���߼���','TCB+WXJP',1,'',8000,5,0,1,'')
SET IDENTITY_Insert PointGift OFF
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
--SysShop
DELETE SysShop where ShopID>1  DBCC CHECKIDENT(SysShop,RESEED,1)
--SysShopAuthority
DELETE SysShopAuthority where ShopAuthorityID>1 DBCC CHECKIDENT(SysShopAuthority,RESEED,1)
UPDATE SysShopAuthority SET ShopAuthorityData = '1' WHERE ShopAuthorityID = 1
--SysUser
DELETE SysUser where UserID>1  DBCC CHECKIDENT(SysUser,RESEED,1)
--OrderTime
DELETE OrderTime DBCC CHECKIDENT(OrderTime,RESEED,0)
--Message
DELETE Message DBCC CHECKIDENT(Message,RESEED,0)
--GiftExchange
DELETE GiftExchange DBCC CHECKIDENT(GiftExchange,RESEED,0)
--GiftExchangeDetail
DELETE GiftExchangeDetail DBCC CHECKIDENT(GiftExchangeDetail,RESEED,0)
--SysUserWork
DELETE SysUserWork DBCC CHECKIDENT(SysUserWork,RESEED,0)
--WeiXinRule
DELETE WeiXinRule DBCC CHECKIDENT(WeiXinRule,RESEED,1);
INSERT INTO dbo.WeiXinRule(RuleNUmber,RuleNewsType,RuleDesc,RuleContent,RuleUserID,RuleCreateTime)VALUES(1,'text','�ظ�1 ����/��ʾ��Ա��','�뷢�������ֻ�����','1',GETDATE());
INSERT INTO dbo.WeiXinRule(RuleNUmber,RuleNewsType,RuleDesc,RuleContent,RuleUserID,RuleCreateTime)VALUES(2,'text','�ظ�2 ���л�Աת΢�Ż�Ա','�뷢�������ֻ��Ż򿨺�','1',GETDATE());
--WeiXinNews
DELETE WeiXinNews DBCC CHECKIDENT(WeiXinNews,RESEED,0)
--WeiXinLog
DELETE WeiXinLog DBCC CHECKIDENT(WeiXinNews,RESEED,0)
--GoodsClassDiscount
DELETE GoodsClassDiscount DBCC CHECKIDENT(GoodsClassDiscount,RESEED,0)
SET IDENTITY_Insert GoodsClassDiscount ON
insert into GoodsClassDiscount(ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) values (1,1,0,1,1.00,1.00)
insert into GoodsClassDiscount(ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) values (2,2,0,1,1.00,1.00)
insert into GoodsClassDiscount(ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) values (3,3,0,1,1.00,1.00)
insert into GoodsClassDiscount(ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) values (4,4,0,1,1.00,1.00)
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
DELETE SysGroupAuthority where GroupID>1
--SysGroup
DELETE SysGroup where GroupID>1
--EmailLog
DELETE EmailLog DBCC CHECKIDENT(EmailLog,RESEED,0)

--΢��start
--Proposal ��Ա����
DELETE Proposal DBCC CHECKIDENT(Proposal,RESEED,0)
--ProductCenter ��Ʒ����
DELETE ProductCenter DBCC CHECKIDENT(ProductCenter,RESEED,0)
--SymbolShow ����չʾ
DELETE SymbolShow DBCC CHECKIDENT(SymbolShow,RESEED,0)
--Promotions �Żݻ
DELETE Promotions DBCC CHECKIDENT(Promotions,RESEED,0)
--MemberExplanation �̼ҹ���
DELETE MemberExplanation DBCC CHECKIDENT(MemberExplanation,RESEED,0)
--MicroWebsiteGiftExchange �һ���� �һ�ͳ��
DELETE MicroWebsiteGiftExchange DBCC CHECKIDENT(MicroWebsiteGiftExchange,RESEED,0)
--MicroWebsiteGiftExchangeDetail
DELETE MicroWebsiteGiftExchangeDetail DBCC CHECKIDENT(MicroWebsiteGiftExchangeDetail,RESEED,0)
--MicroWebsiteGoodsClass ��Ʒ����
DELETE MicroWebsiteGoodsClass DBCC CHECKIDENT(MicroWebsiteGoodsClass,RESEED,0)
--MicroWebsiteGoods ��Ʒ�б�
DELETE MicroWebsiteGoods DBCC CHECKIDENT(MicroWebsiteGoods,RESEED,0)
--MicroWebsiteOrderLog ���Ѽ�¼ �������
DELETE MicroWebsiteOrderLog DBCC CHECKIDENT(MicroWebsiteOrderLog,RESEED,0)
--MicroWebsiteOrderLogDetail
DELETE MicroWebsiteOrderLogDetail DBCC CHECKIDENT(MicroWebsiteOrderLogDetail,RESEED,0)
--WeiXinMenu
DELETE WeiXinMenu where MenuID>7 DBCC CHECKIDENT(WeiXinMenu,RESEED,7)
--΢��end

--�̼�
--SysShopBuyCard �̼ҹ���
DELETE SysShopBuyCard DBCC CHECKIDENT(SysShopBuyCard,RESEED,0)
--SysShopSettlement �̼ҽ���
DELETE SysShopSettlement DBCC CHECKIDENT(SysShopSettlement,RESEED,0)
--SysShopPointLog �̼һ��ֱ䶯��¼
DELETE SysShopPointLog DBCC CHECKIDENT(SysShopPointLog,RESEED,0)
--SysShopCmsLog  �̼Ҷ��ű䶯��¼
DELETE SysShopCmsLog DBCC CHECKIDENT(SysShopCmsLog,RESEED,0)

--�̼�