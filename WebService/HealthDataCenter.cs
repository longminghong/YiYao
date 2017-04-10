using CardReader;
using MTM.SDK;
using MTM.SDK.Contract;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var appid = "Bk_ByrUY";
            var appkey = "c96b1296fdfc415024bebfcd2264b486";
            mSdk = new MTMWebClient(appid, appkey);
        }

        public async Task<DataResult<MemberInfo>> GetMemberInfoBySsnAsync(string ssn)
        {
            if (mCachedMemberInfo != null)
                return mCachedMemberInfo;
            var data = await mSdk.GetMemberInfoBySsnAsync(ssn);
            if(data != null && data.error.code != 0)
            {
                return null;
            }
            mCachedMemberInfo = data;
            return data;
        }

        public async Task<DataResult<HealthData>> GetHealthDataAsync()
        {
            if (mHealthData != null)
                return mHealthData;
            if (mCachedMemberInfo == null)
                return null;
            var data = await mSdk.GetHealthDataAsync(mCachedMemberInfo.data.mid);
            if(data == null)
            {
                return null;
            }
            if(data.error.code == 0)
            {
                mHealthData = data;
            }
            return data;
        }

        public async Task<DataResult<HealthData>> InquiredHealthDataAsync(InquiredForm form)
        {
            if (mHealthData != null)
                return mHealthData;
            if (mCachedMemberInfo == null)
                return null;
            form.mid = mCachedMemberInfo.data.mid;
            form.uid = mCachedMemberInfo.data.uid;
            var data = await mSdk.InquiredHealthDataAsync(form);
            if (data != null && data.error.code == 0)
            {
                mHealthData = data;
            }
            return data;
        }

        
    }
}
