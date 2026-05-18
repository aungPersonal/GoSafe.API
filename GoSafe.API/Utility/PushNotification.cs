using Firebase.Database;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using GoSafe.API.Utility.Models;
using GoSafe.LoggerTool;
using System.Text.Json;

namespace GoSafe.API.Utility
{
    public class PushNotification
    {
        public readonly Logger logger;
        private FirebaseMessaging messaging;
        private FirebaseApp app;

        public PushNotification(Logger log)
        {
            this.logger = log;
            app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("Firebase/ServiceAccount.json"),
            });
            messaging = FirebaseMessaging.GetMessaging(app);
        }

        public async Task SendFCMNotification(FcmNotificationRequest request)
        {
            try
            {
                Dictionary<string, string> data = GetReqData(request);

                var message = new Message
                {
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = request.Title,
                        Body = request.Body
                    },
                    Data = data,
                    Topic = request.Topic
                };

                var response = await messaging.SendAsync(message);
                logger.Debug($"SendFCMNotification => {JsonSerializer.Serialize(response)}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error sending FCM notification.");
            }
        }

        private static Dictionary<string, string> GetReqData(FcmNotificationRequest request)
        {
            return new Dictionary<string, string>
            {
                ["Message"] = request.Body,
                ["NotiTypeId"] = request.Type.ToString(),
                ["Attribute1"] = request.Attribute1 ?? string.Empty,
                ["Attribute2"] = request.Attribute2 ?? string.Empty,
                ["Attribute3"] = request.Attribute3 ?? string.Empty,
                ["detailpage"] = request.DetailPage,
                ["notificationid"] = request.NotificationId.ToString(),
                ["ThumbnailUrl"] = request.ThumbnailUrl ?? string.Empty,
            };
        }

        public async Task SendFCMNotificationToAll(FcmNotificationRequest request, List<string> topicList)
        {
            try
            {
                Dictionary<string, string> data = GetReqData(request);

                foreach (var topic in topicList)
                {
                    var message = new Message
                    {
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = request.Title,
                            Body = request.Body
                        },
                        Data = data,
                        Topic = topic
                    };

                    var response = await messaging.SendAsync(message);
                    logger.Debug($"SendFCMNotification => {JsonSerializer.Serialize(response)}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error sending FCM notifications to topic list.");
            }
        }
    }
}
