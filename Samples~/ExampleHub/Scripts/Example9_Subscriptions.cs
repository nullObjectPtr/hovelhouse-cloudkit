#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
using System.Collections;
using UnityEngine;
using HovelHouse.CloudKit;
using System.Linq;

public class Example9_Subscriptions : MonoBehaviour
{
    const string recordType = "SubscriptionRecordType";
    private CKDatabase database;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log("Example 9 - Subscriptions");

        Debug.Log("Requesting notification token");

        ICloudNotifications.RequestNotificationToken((tokenBytes, error) => {
            if(error != null)
            {
                Debug.LogError("Failed to get push notification token: " + error.LocalizedDescription);
            }
            else
            {
                Debug.LogError("Got push notification token");
            }
        });

        ICloudNotifications.SetRemoteNotificationHandler((p) => {
            Debug.Log("remote notification handler called");
        });

        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        // You only need to register a subscription ONCE
        // So check that a subscription exists before trying to add one
        database.FetchAllSubscriptionsWithCompletionHandler(OnSubscriptionsFetched);

        ICloudNotifications.SetRemoteNotificationHandler(OnQueryNotification);

        yield break;
    }

    private void OnSubscriptionsFetched(CKSubscription[] subscriptions, NSError error)
    {
        if(error != null)
        {
            Debug.LogError(error.LocalizedDescription);
            return;
        }

        if(subscriptions.Length > 0)
        {
            var subscriptionIds = subscriptions.Select(sub => sub.SubscriptionID.ToString()).ToArray();
            Debug.LogFormat("You have the following subscriptions: {0}",
                string.Join(",", subscriptionIds));
            return;
        }
        
        Debug.Log("No subscriptions detected - making a new one");

        // Make sure a record of this type exists on the server before attempting
        // a subscription, or the subscription will fail
        database.SaveRecord(new CKRecord(recordType), (record, error2) => {
            if(error2 != null)
            {
                Debug.LogError(error2.LocalizedDescription);
                return;
            }

            // You only need to do this ONCE
            CreateSubscription();
        });  
    }

    private void OnQueryNotification(CKNotification notification)
    {   
        Debug.Log(string.Format("Recieved notification: {0} {1}", notification.NotificationID, notification.NotificationType));

        if (notification is CKQueryNotification queryNotification)
        {
            Debug.Log("Notification Reason: " + queryNotification.QueryNotificationReason);
            Debug.Log("RecordID: " + queryNotification.RecordID.RecordName);
        }
    }

    private void CreateSubscription()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var predicate = NSPredicate.PredicateWithValue(true);

        var notificationInfo = new CKNotificationInfo();

        var querySubscription = new CKQuerySubscription(
            recordType,
            predicate,
            CKQuerySubscriptionOptions.FiresOnRecordCreation |
            CKQuerySubscriptionOptions.FiresOnRecordDeletion |
            CKQuerySubscriptionOptions.FiresOnRecordUpdate);

        querySubscription.NotificationInfo = notificationInfo;

        database.SaveSubscription(querySubscription, OnSubscriptionSaved);

        //alternatively...you can use a CKModifySubscriptionsOperation

        //var op = new CKModifySubscriptionsOperation(new[] { querySubscription }, null);
        //op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
        //op.ModifySubscriptionsCompletionBlock = OnSubscriptionModified;
        //database.AddOperation(op);
    }

    private void OnSubscriptionModified(
        CKSubscription[] savedSubscriptions,
        string[] deletedSubscriptionIds,
        NSError error)
    {
        if(error == null)
        {
            Debug.Log(string.Format("Saved {0} subscriptions with ids: ({1})",
                savedSubscriptions.Length,
                string.Join(",", savedSubscriptions.Select(x => x.SubscriptionID).ToArray())
                ));
        }
        else
        {
            Debug.LogError(error.LocalizedDescription);
        }
    }

    private void OnSubscriptionSaved(CKSubscription subscription, NSError error)
    {        
        if (error == null)
        {
            Debug.Log(string.Format("subscription saved with id: {0} of type: {1}",
                subscription.SubscriptionID, subscription.SubscriptionType));
        }
        else
        {
            Debug.LogError("error saving subscription: " + error.LocalizedDescription);
        }
    }
}
#endif