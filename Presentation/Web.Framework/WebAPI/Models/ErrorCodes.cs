using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.WebAPI.Models
{
    public class ErrorCodes
    {
        #region 不存在

        /// <summary>
        /// 档案不存在
        /// </summary>
        public const string ProfileNotExists = nameof(ProfileNotExists);

        /// <summary>
        /// 会员卡不存在
        /// </summary>
        public const string MembershipCardNotExists = nameof(MembershipCardNotExists);

        /// <summary>
        /// 会员卡号不存在
        /// </summary>
        public const string MembershipCardNumberNotExists = nameof(MembershipCardNumberNotExists);

        /// <summary>
        /// 卡号和Id不属于同一张会员卡
        /// </summary>
        public const string MembershipCardIsDifferent = nameof(MembershipCardIsDifferent);


        /// <summary>
        /// 礼品卡激活码不存在
        /// </summary>
        public const string GiftCardPinNotExists = nameof(GiftCardPinNotExists);

        /// <summary>
        /// 无效的礼品卡类型
        /// </summary>
        public const string InvalidGiftCardType = nameof(InvalidGiftCardType);

        /// <summary>
        /// 充值记录不存在
        /// </summary>
        public const string RechargeRecordNotExists = nameof(RechargeRecordNotExists);

        /// <summary>
        /// 不存在的会员卡类型
        /// </summary>
        public const string MembershipCardTypeNotExists = nameof(MembershipCardTypeNotExists);

        /// <summary>
        /// 不存在的会员卡级别
        /// </summary>
        public const string MembershipCardLevelNotExists = nameof(MembershipCardLevelNotExists);

        /// <summary>
        /// 会员卡来源不存在
        /// </summary>
        public const string EnrollmentChannelCodeNotExists = nameof(EnrollmentChannelCodeNotExists);

        /// <summary>
        /// 非最低级别的会员卡级别
        /// </summary>
        public const string NotTheLowestCardLevel = nameof(NotTheLowestCardLevel);

        /// <summary>
        /// 该会员卡已被注销
        /// </summary>
        public const string MembershipCardIsCancelled = nameof(MembershipCardIsCancelled);

        /// <summary>
        /// 不可用的会员卡
        /// </summary>
        public const string UnavailableMembershipCard = nameof(UnavailableMembershipCard);

        /// <summary>
        /// 该账户不存在
        /// </summary>
        public const string AccountNotExists = nameof(AccountNotExists);

        /// <summary>
        /// 该账户不是积分账户
        /// </summary>
        public const string AccountNotPointAccount = nameof(AccountNotPointAccount);

        /// <summary>
        /// 该账户不是积分账户
        /// </summary>
        public const string IntroducerNotPointAccount = nameof(IntroducerNotPointAccount);

        /// <summary>
        /// 该外部会员提供商不存在
        /// </summary>
        public const string ProviderNotExists = nameof(ProviderNotExists);

        /// <summary>
        /// 不存在的会员三方关系
        /// </summary>
        public const string ExternalMembershipExternalMembership = nameof(ExternalMembershipExternalMembership);

        /// <summary>
        /// FFPId不存在
        /// </summary>
        public const string FFPIdNotExists = nameof(FFPIdNotExists);

        /// <summary>
        /// FFP级别不存在
        /// </summary>
        public const string FFPLevelIdNotExists = nameof(FFPLevelIdNotExists);

        /// <summary>
        /// 会员偏好大类不存在
        /// </summary>
        public const string PreferenceCategoryNotExists = nameof(PreferenceCategoryNotExists);

        /// <summary>
        /// 会员偏好小类不存在
        /// </summary>
        public const string PreferenceNotExists = nameof(PreferenceNotExists);

        /// <summary>
        /// 该偏好小类不是会员本身可维护的
        /// </summary>
        public const string PreferenceNotBelongToProfile = nameof(PreferenceNotBelongToProfile);

        /// <summary>
        /// 偏好小类不属于该偏好大类
        /// </summary>
        public const string PreferenceNotBelongToPreferenceCategory = nameof(PreferenceNotBelongToPreferenceCategory);

        /// <summary>
        /// 偏好大小类重复
        /// </summary>
        public const string PreferenceCategoryPreferenceRepeat = nameof(PreferenceCategoryPreferenceRepeat);

        /// <summary>
        /// FFP卡类型不存在
        /// </summary>
        public const string FFPTypeNotExists = nameof(FFPTypeNotExists);

        /// <summary>
        /// FFP卡级别不存在
        /// </summary>
        public const string FFPLevelNotExists = nameof(FFPLevelNotExists);

        /// <summary>
        /// FFP卡级别与FFP卡类型不匹配
        /// </summary>
        public const string FFPLevelTypeNotExists = nameof(FFPLevelTypeNotExists);
        /// <summary>
        /// 航空卡号重复
        /// </summary>
        public const string FFPCardNumberExists = nameof(FFPCardNumberExists);
        /// <summary>
        /// 提供商和第三方会员信息不匹配
        /// </summary>
        public const string ProviderProviderKeyMismatching = nameof(ProviderProviderKeyMismatching);
        /// <summary>
        /// 第三方会员信息不存在
        /// </summary>
        public const string ProviderKeyNotExists = nameof(ProviderKeyNotExists);
        /// <summary>
        /// 提供商code不能为空
        /// </summary>
        public const string ProviderCodeNotNull = nameof(ProviderCodeNotNull);
        /// <summary>
        /// 第三方会员信息不能为空
        /// </summary>
        public const string ProviderKeyNotNull = nameof(ProviderKeyNotNull);

        /// <summary>
        /// 第三方会员信息不合法
        /// </summary>
        public const string WrongProviderKey = nameof(WrongProviderKey);

        /// <summary>
        /// 会员id不能为空
        /// </summary>
        public const string ProfileIdNotNull = nameof(ProfileIdNotNull);

        #endregion

        #region 已存在

        /// <summary>
        /// 手机号已存在
        /// </summary>
        public const string MobileNumberAlreadyExists = nameof(MobileNumberAlreadyExists);

        /// <summary>
        /// Email 已存在
        /// </summary>
        public const string EmailAlreadyExists = nameof(EmailAlreadyExists);

        /// <summary>
        /// 证件号已存在
        /// </summary>
        public const string IdNumberAlreadyExists = nameof(IdNumberAlreadyExists);
        /// <summary>
        /// 相同卡类型、卡号已存在
        /// </summary>
        public const string AlreadyExists = nameof(AlreadyExists);

        #endregion

        #region Common

        /// <summary>
        /// 未知错误
        /// </summary>
        public const string UnknownError = nameof(UnknownError);

        /// <summary>
        /// 密码错误
        /// </summary>
        public const string WrongPassword = nameof(WrongPassword);

        /// <summary>
        /// 错误的三方会员id
        /// </summary>
        public const string ProviderKeyError = nameof(ProviderKeyError);

        /// <summary>
        /// 错误的时间
        /// </summary>
        public const string WrongDate = nameof(WrongDate);

        /// <summary>
        /// 错误的页数
        /// </summary>
        public const string WrongPage = nameof(WrongPage);

        /// <summary>
        /// 错误的会员卡号或密码
        /// </summary>
        public const string WrongMembershipCardNumberOrPassword = nameof(WrongMembershipCardNumberOrPassword);

        /// <summary>
        /// 余额不足
        /// </summary>
        public const string InsufficientBalance = nameof(InsufficientBalance);

        /// <summary>
        /// 礼品卡库存不足
        /// </summary>
        public const string GiftCardUnderStock = nameof(GiftCardUnderStock);

        /// <summary>
        /// 请求信息错误
        /// </summary>
        public const string BadRequest = nameof(BadRequest);

        /// <summary>
        /// 不是当日订单
        /// </summary>
        public const string NotTodayBilling = nameof(NotTodayBilling);

        /// <summary>
        /// 查找的内容不存在
        /// </summary>
        public const string NotFound = nameof(NotFound);

        /// <summary>
        /// 券号不存在
        /// </summary>
        public const string NotExistCouponSerialNumber = nameof(NotExistCouponSerialNumber);

        /// <summary>
        /// 查找的消费不存在
        /// </summary>
        public const string NotFoundConsume = nameof(NotFoundConsume);

        /// <summary>
        /// 不存在支付信息
        /// </summary>
        public const string NotExistPaymentInformation = nameof(NotExistPaymentInformation);

        /// <summary>
        /// 地点没有绑定到消费类型
        /// </summary>
        public const string PlaceNotBindConsumeType = nameof(PlaceNotBindConsumeType);

        /// <summary>
        /// 会员卡下不存在账户
        /// </summary>
        public const string NoAccount = nameof(NoAccount);

        /// <summary>
        /// 未设置币种
        /// </summary>
        public const string NoCurrency = nameof(NoCurrency);

        /// <summary>
        /// 错误的交易金额
        /// </summary>
        public const string ErrorAmount = nameof(ErrorAmount);

        /// <summary>
        /// 找不到对应的折扣规则
        /// </summary>
        public const string NotFoundDiscount = nameof(NotFoundDiscount);

        /// <summary>
        /// 内容冲突
        /// </summary>
        public const string Conflict = nameof(Conflict);

        /// <summary>
        /// 券已经被使用
        /// </summary>
        public const string CouponUsed = nameof(CouponUsed);

        /// <summary>
        /// 券还没被使用
        /// </summary>
        public const string CouponNotUsed = nameof(CouponNotUsed);

        /// <summary>
        /// 券已经过期
        /// </summary>
        public const string CouponExpired = nameof(CouponExpired);

        /// <summary>
        /// 券不在使用日期内
        /// </summary>
        public const string NotOnUseDate = nameof(NotOnUseDate);

        /// <summary>
        /// 只能取消当天使用的券
        /// </summary>
        public const string NotTodayUse = nameof(NotTodayUse);

        /// <summary>
        /// 券没有绑定会员
        /// </summary>
        public const string CouponNotBindProfile = nameof(CouponNotBindProfile);

        /// <summary>
        /// 券没有匹配的卡类型
        /// </summary>
        public const string CouponNotMapMembershipCardType = nameof(CouponNotMapMembershipCardType);
        /// <summary>
        /// 此会员卡没有此消费
        /// </summary>
        public const string NoConsume = nameof(NoConsume);

        /// <summary>
        /// 不存在储值账户
        /// </summary>
        public const string NotExistStoreAccount = nameof(NotExistStoreAccount);

        /// <summary>
        /// 要取消支付的账单的金额跟传入的金额不匹配
        /// </summary>
        public const string AmountDiff = nameof(AmountDiff);

        /// <summary>
        /// 要取消支付的账单的卡号跟传入的卡号不匹配
        /// </summary>
        public const string CardNumberDiff = nameof(CardNumberDiff);

        /// <summary>
        /// 配置冲突
        /// </summary>
        public const string ConfigurationConflict = nameof(ConfigurationConflict);

        /// <summary>
        /// 纳税人识别号冲突
        /// </summary>
        public const string TaxNumberConflict = nameof(TaxNumberConflict);

        /// <summary>
        /// 重复的消费记录
        /// </summary>
        public const string RepeatConsume = nameof(RepeatConsume);

        /// <summary>
        /// 没有支付功能
        /// </summary>
        public const string NoFunctionCheckout = nameof(NoFunctionCheckout);

        /// <summary>
        /// 卡被冻结
        /// </summary>
        public const string CardFrozen = nameof(CardFrozen);

        /// <summary>
        /// 销卡
        /// </summary>
        public const string CardCanceled = nameof(CardCanceled);

        /// <summary>
        /// 账单已支付
        /// </summary>
        public const string BillPaymented = nameof(BillPaymented);

        /// <summary>
        /// 此卡类型没有设置支付规则
        /// </summary>
        public const string NoSetPaymentRule = nameof(NoSetPaymentRule);

        /// <summary>
        /// 不可处理的实体
        /// </summary>
        public const string UnprocessableEntity = nameof(UnprocessableEntity);

        /// <summary>
        /// 不支持会员卡类型
        /// </summary>
        public const string NotSupportMembershipCardType = nameof(NotSupportMembershipCardType);

        /// <summary>
        /// 不是可用状态
        /// </summary>
        public const string NotDistribute = nameof(NotDistribute);

        /// <summary>
        /// 无效的会员卡
        /// </summary>
        public const string InvalidMembershipCard = nameof(InvalidMembershipCard);

        /// <summary>
        /// 支付金额与储值金额不匹配
        /// </summary>
        public const string MisMatching = nameof(MisMatching);
        /// <summary>
        /// 错误的支付方式
        /// </summary>
        public const string ErrorPayment = nameof(ErrorPayment);

        /// <summary>
        /// 不存在的交易地点
        /// </summary>
        public const string NotExistsPlace = nameof(NotExistsPlace);

        /// <summary>
        /// 不存在的酒店
        /// </summary>
        public const string NotExistsHotel = nameof(NotExistsHotel);

        /// <summary>
        /// 不存在的性别代码
        /// </summary>
        public const string NotExistsGenderCode = nameof(NotExistsGenderCode);

        /// <summary>
        /// 不存在的称呼代码
        /// </summary>
        public const string NotExistsTitleCode = nameof(NotExistsTitleCode);

        /// <summary>
        /// 不存在的语言代码
        /// </summary>
        public const string NotExistsLanguageCode = nameof(NotExistsLanguageCode);

        /// <summary>
        /// 不存在的国籍格式
        /// </summary>
        public const string NotExistsNationalityCode = nameof(NotExistsNationalityCode);

        /// <summary>
        /// 不存在的证件类型
        /// </summary>
        public const string NotExistsIdTypeCode = nameof(NotExistsIdTypeCode);

        /// <summary>
        /// 不存在的国家代码
        /// </summary>
        public const string NotExistsCountryCode = nameof(NotExistsCountryCode);

        /// <summary>
        /// 不存在的省份代码
        /// </summary>
        public const string NotExistsProvinceCode = nameof(NotExistsProvinceCode);

        /// <summary>
        /// 不存在的城市代码
        /// </summary>
        public const string NotExistsCityCode = nameof(NotExistsCityCode);

        /// <summary>
        /// 必须指定国家代码
        /// </summary>
        public const string MustSpecifyCountryCode = nameof(MustSpecifyCountryCode);

        /// <summary>
        /// 不存在的外部会员类型
        /// </summary>
        public const string NotExistsProviderType = nameof(NotExistsProviderType);

        /// <summary>
        /// 输入信息不完整
        /// </summary>
        public const string IncompleteInformation = nameof(IncompleteInformation);

        /// <summary>
        /// 无查询信息
        /// </summary>
        public const string MissingQueryParameters = nameof(MissingQueryParameters);

        /// <summary>
        /// 会员卡未绑定会员
        /// </summary>
        public const string NotBindProfile = nameof(NotBindProfile);

        /// <summary>
        /// 取消核销券的人和核销券的人不是同一人
        /// </summary>
        public const string NotCouponUser = nameof(NotCouponUser);

        /// <summary>
        /// Pos交易地点在没有映射关系
        /// </summary>
        public const string PosOutletNoMap = nameof(PosOutletNoMap);

        /// <summary>
        /// 交易地点限制
        /// </summary>
        public const string PlaceLimited = nameof(PlaceLimited);

        /// <summary>
        /// Pos订单类型在没有映射关系
        /// </summary>
        public const string PosOrderTypeMap = nameof(PosOrderTypeMap);

        /// <summary>
        /// 没有设置餐段
        /// </summary>
        public const string NoSetMealPeriod = nameof(NoSetMealPeriod);

        /// <summary>
        /// 更新数据库失败
        /// </summary>
        public const string UpdateDBError = nameof(UpdateDBError);

        /// <summary>
        /// 不存在的货币类型
        /// </summary>
        public const string NotExistsCurrencyCode = nameof(NotExistsCurrencyCode);

        /// <summary>
        /// 不存在的消费类型
        /// </summary>
        public const string NotExistsConsumeTypeCode = nameof(NotExistsConsumeTypeCode);

        /// <summary>
        /// 不允许绑定FFP
        /// </summary>
        public const string NotBindingFFP = nameof(NotBindingFFP);

        /// <summary>
        /// 重复ProviderKey
        /// </summary>
        public const string RepeatProviderKey = nameof(RepeatProviderKey);

        /// <summary>
        /// FFP卡号不符合规则
        /// </summary>
        public const string FFPNumberError = nameof(FFPNumberError);

        /// <summary>
        /// 不存在的FFP卡号
        /// </summary>
        public const string FFPNumberNotExist = nameof(FFPNumberNotExist);

        /// <summary>
        /// 取消绑定第三方会员身份失败
        /// </summary>
        public const string CancelExternalMembershipError = nameof(CancelExternalMembershipError);
        #endregion

        #region POS API Only

        /// <summary>
        /// 写入消费历史成功
        /// </summary>
        public const string InsertConsumeHistoryOk = nameof(InsertConsumeHistoryOk);

        /// <summary>
        /// 更新消费历史成功
        /// </summary>
        public const string UpdateConsumeHistoryOk = nameof(UpdateConsumeHistoryOk);

        #endregion

        #region Web API Only

        /// <summary>
        /// 不允许为负值
        /// </summary>
        public const string CannotBeNegative = nameof(CannotBeNegative);

        /// <summary>
        /// 没有可售的会员卡
        /// </summary>
        public const string UnableToFindAAvailableCard = nameof(UnableToFindAAvailableCard);

        /// <summary>
        /// 该卡没有支付功能
        /// </summary>
        public const string CardNotHavePaymentFunction = nameof(CardNotHavePaymentFunction);

        /// <summary>
        /// 会员卡已存在密码
        /// </summary>
        public const string MembershipCardHavePassword = nameof(MembershipCardHavePassword);

        /// <summary>
        /// 酒店不存在
        /// </summary>
        public const string HotelCodeIsNotExist = nameof(HotelCodeIsNotExist);

        /// <summary>
        /// 交易场所不存在
        /// </summary>
        public const string PlaceCodeIsNotExist = nameof(PlaceCodeIsNotExist);

        /// <summary>
        /// 交易场所所在酒店与传入酒店不相同
        /// </summary>
        public const string PlaceIsNotBelongHotel = nameof(PlaceIsNotBelongHotel);

        /// <summary>
        /// 该发票不属于此会员
        /// </summary>
        public const string TitleIsNotBelongProfile = nameof(TitleIsNotBelongProfile);

        /// <summary>
        /// 会员已存在密码
        /// </summary>
        public const string ProfileHavePassword = nameof(ProfileHavePassword);

        /// <summary>
        /// 查找的交易信息不属于此会员
        /// </summary>
        public const string TransactionNotBelongProfile = nameof(TransactionNotBelongProfile);

        /// <summary>
        /// 找不到会员的发票抬头信息
        /// </summary>
        public const string NotFoundProfileReceiptTitle = nameof(NotFoundProfileReceiptTitle);

        /// <summary>
        /// 找不到交易单号
        /// </summary>
        public const string NotFoundTransactionNumber = nameof(NotFoundTransactionNumber);

        /// <summary>
        /// 找不到交易流水记录
        /// </summary>
        public const string NotFoundStoredValue = nameof(NotFoundStoredValue);

        /// <summary>
        /// 找不到会员卡交易记录
        /// </summary>
        public const string NotFoundMembershipCardTransaction = nameof(NotFoundMembershipCardTransaction);

        /// <summary>
        /// 不允许有多张卡
        /// </summary>
        public const string IsNotHaveAnyCards = nameof(IsNotHaveAnyCards);

        /// <summary>
        /// 兑换券类型不存在
        /// </summary>
        public const string NotExistCouponType = nameof(NotExistCouponType);

        /// <summary>
        /// 兑换券库存不足
        /// </summary>
        public const string CouponInventoryInsufficient = nameof(CouponInventoryInsufficient);

        /// <summary>
        /// 积分账户不存在
        /// </summary>
        public const string PointsAccountNotExist = nameof(PointsAccountNotExist);


        /// <summary>
        /// 账户积分不足
        /// </summary>
        public const string AccountPointsInsufficient = nameof(AccountPointsInsufficient);


        /// <summary>
        /// 会员卡未绑定会员
        /// </summary>
        public const string MembershipCardNotBind = nameof(MembershipCardNotBind);

        /// <summary>
        /// 不存在的投放渠道
        /// </summary>
        public const string NotExistCouponChannel = nameof(NotExistCouponChannel);

        /// <summary>
        /// 券不能在此渠道投放
        /// </summary>
        public const string CouponChannelNotUseCouponType = nameof(CouponChannelNotUseCouponType);


        /// <summary>
        /// 兑换券数量大于0
        /// </summary>
        public const string CouponSumGreaterThanZero = nameof(CouponSumGreaterThanZero);

        /// <summary>
        /// 兑换失败
        /// </summary>
        public const string PointsExchangeCouponError = nameof(PointsExchangeCouponError);


        /// <summary>
        /// 卡类型下不存在此类型兑换券
        /// </summary>
        public const string CouponTypeOutMembershipCardType = nameof(CouponTypeOutMembershipCardType);

        /// <summary>
        /// 兑换券不存在
        /// </summary>
        public const string CouponNotExist = nameof(CouponNotExist);


        /// <summary>
        /// 兑换券已经被使用
        /// </summary>
        public const string CouponIsUsed = nameof(CouponIsUsed);

        /// <summary>
        /// 兑换券已经过期
        /// </summary>
        public const string CouponIsExpired = nameof(CouponIsExpired);

        /// <summary>
        /// 兑换券未投放
        /// </summary>
        public const string CouponIsNotPut = nameof(CouponIsNotPut);

        /// <summary>
        /// 会员卡不能使用此兑换券
        /// </summary>
        public const string MembershipCardNotUseCoupon = nameof(MembershipCardNotUseCoupon);


        /// <summary>
        /// 核销券失败
        /// </summary>
        public const string UseCouponIsFalied = nameof(UseCouponIsFalied);

        /// <summary>
        /// 会员与券拥有者不同
        /// </summary>
        public const string ProfileNotHaveCoupon = nameof(ProfileNotHaveCoupon);

        /// <summary>
        /// 此会员卡不属于会员
        /// </summary>
        public const string ProfileNotHaveThisCard = nameof(ProfileNotHaveThisCard);


        /// <summary>
        /// 兑换券类型不能为空
        /// </summary>
        public const string CouponTypeIsNull = nameof(CouponTypeIsNull);


        /// <summary>
        /// 取消兑换券失败
        /// </summary>
        public const string CancelExchangeCouponFailed = nameof(CancelExchangeCouponFailed);

        /// <summary>
        /// 兑换券不能在此交易场所使用
        /// </summary>
        public const string CouponNotUseInPlace = nameof(CouponNotUseInPlace);

        /// <summary>
        /// 兑换券Id不能为空
        /// </summary>
        public const string CouponIdNotNull = nameof(CouponIdNotNull);
        /// <summary>
        /// 交易场所不能为空
        /// </summary>
        public const string PlaceCodeIsNotNull = nameof(PlaceCodeIsNotNull);
        /// <summary>
        /// 兑换券号不能为空
        /// </summary>
        public const string CouponNumberIsNotNull = nameof(CouponNumberIsNotNull);
        /// <summary>
        /// 兑换券类型Id不能为空
        /// </summary>
        public const string CouponTypeIdIsNull = nameof(CouponTypeIdIsNull);

        /// <summary>
        /// 兑换与取消兑换渠道不同
        /// </summary>
        public const string CouponChannelDiff = nameof(CouponChannelDiff);

        /// <summary>
        /// 会员卡被冻结
        /// </summary>
        public const string MembershipCardIsFreeze = nameof(MembershipCardIsFreeze);


        /// <summary>
        /// 卡号与卡Id不属于同一会员卡
        /// </summary>
        public const string MembershipCardIdAndNumberIsDiff = nameof(MembershipCardIdAndNumberIsDiff);

        #endregion
    }
}
