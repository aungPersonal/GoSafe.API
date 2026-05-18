using Firebase.Database;
using Firebase.Database.Query;
using GoSafe.API.Common;
using GoSafe.Common;
using GoSafe.LoggerTool;

namespace GoSafe.API.Utility
{
    public class FirebaseDbHelper
    {
        private FirebaseClient firebase = null;
        private Logger logger;
        public FirebaseDbHelper(Logger logger)
        {
            firebase = new FirebaseClient(CommonConstants.FirebaseDbUrl);
            //firebase = new FirebaseClient(CommonConstants.FirebaseDbUrl, new FirebaseOptions
            //{
            //    AuthTokenAsyncFactory = () => Task.FromResult(CommonConstants.SecretKey)
            //});
            this.logger = logger;
        }

        public async Task AddOrUpdateNotiNewCount(string userId, int count)
        {
            try
            {
                await firebase!
                    .Child("NotiNewCount")
                    .Child(userId)
                    .PutAsync(new { UserId = userId, Count = count });

            }
            catch (Exception ex)
            {
                logger.Error($"NotiNewCount count updated for UserId: {userId}", ex);
            }

        }

        public async Task<object> GetFirebaseNoti()
        {
            try
            {
                var notification = await firebase
                   .Child("NotiNewCount")
                   .Child(1.ToString())
                   .OnceSingleAsync<object>();
                return notification;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
