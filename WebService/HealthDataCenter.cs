using CardReader;
using MTM.SDK;
using MTM.SDK.Contract;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Event;

namespace WebService
{
    public class HealthDataService
    {
        private MTMWebClient mSdk;
        private DataResult<MemberInfo> mCachedMemberInfo;
        private DataResult<HealthData> mHealthData;
        private IDCard mIDCard;
        private IEventAggregator mEventAggregator;

        public void SetIDCard(IDCard idCard)
        {
            mIDCard = idCard;
        }

        public HealthDataService(EventAggregator eventAggregator)
        {
            mEventAggregator = eventAggregator;
            //var appid = "Bk_ByrUY";
            var appid = "SyccthZn";
            //var appkey = "c96b1296fdfc415024bebfcd2264b486";
            var appkey = "097e8751c3c183edf602f867a5326559";
            
            mSdk = new MTMWebClient(appid, appkey);
        }

        public async Task<DataResult<MemberInfo>> GetMemberInfoBySsnAsync(string ssn)
        {
            //if (mCachedMemberInfo != null)
            //    return mCachedMemberInfo;
            var data = await mSdk.GetMemberInfoBySsnAsync(ssn);
            if (Check(data))
            {
                mCachedMemberInfo = data;
            }
            else {
                mCachedMemberInfo = null;
            }
            return data;
        }

        public string GetMemberMid()
        {

            string resultValue = "";
            if (null != mCachedMemberInfo)
            {
                resultValue = mCachedMemberInfo.data.mid;
            }
            return resultValue;
        }
        public string GetMemberCardNo()
        {

            string resultValue = "";
            if (null != mCachedMemberInfo)
            {
                resultValue = mCachedMemberInfo.data.card_no;
            }
            return resultValue;
        }

        public async Task<DataResult<HealthData>> GetHealthDataAsync()
        {
            if (mHealthData != null)
                return mHealthData;
            if (mCachedMemberInfo == null)
                return null;
            var data = await mSdk.GetHealthDataAsync(mCachedMemberInfo.data.mid);
            if(Check(data))
            {
                mHealthData = data;
            }
            return data;
        }

        public async Task<DataResult<HealthData>> InquiredHealthDataAsync(InquiredForm form)
        {
            if (mCachedMemberInfo == null)
                return null;
            form.mid = mCachedMemberInfo.data.mid;
            form.uid = mCachedMemberInfo.data.uid;
            var data = await mSdk.InquiredHealthDataAsync(form);
            Check(data);
            return data;
        }

        private bool Check<T>(DataResult<T> data)
        {
            if (data == null)
            {
                mEventAggregator.GetEvent<WebErrorEvent>().Publish("网络错误");
                return false;
            }
            if (data.error.code != 0)
            {
                mEventAggregator.GetEvent<WebErrorEvent>().Publish(data.error.message);
                return false;
            }
            return true;

        }

        public void SavePhoneNumber(string text)
        {
           
            if (null != text)
            {
                mCachedMemberInfo.data.phone = text;
            }
         
        }
    }
}
