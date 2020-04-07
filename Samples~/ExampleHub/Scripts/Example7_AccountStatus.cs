using UnityEngine;
using HovelHouse.CloudKit;

public class Example7_AccountStatus : MonoBehaviour
{
    private Unsubscriber unsub;
    private UbiquityIdentityToken Token;
    private NSUbiquitousKeyValueStore store;
    private CKAccountStatus? currentAccountStatus;
    private Unsubscriber Unsubscriber;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example 7 - Account Status");

        // This is the Core Foundation way of checking if the user has changed
        // This method is NOT recommended by apple. But is the only option if you
        // are not using CloudKit (and are just using key value storage)

        Token = NSFileManager.DefaultManager().UbiquityIdentityToken;
        Unsubscriber = ICloudNotifications.AddIdentityDidChangeObserver(OnCloudIdentityChanged);

        // If your using cloud kit...apple has this to say:
        // CloudKit clients should not use this token as a way to identify
        // whether the iCloud account is logged in. Instead, use
        // accountStatusWithCompletionHandler: or
        // fetchUserRecordIDWithCompletionHandler:
        // https://developer.apple.com/documentation/foundation/nsfilemanager/1408036-ubiquityidentitytoken?language=objc

        // This is the cloudkit way
        // Listen for the accountStatusChanged notification

        // Apple says... "Notification posted when the status of the signed-in iCloud account may have changed."
        // This notification will fire for a number of reasons, not all of them will be related to signing in or
        // out of iCloud. For instance, toggling the iCloud Drive permission in iCloud settings will cause this
        // to fire

        CKContainer.AddAccountChangedNotificationObserver(AccountStatusChanged);

        // At this point you should sign in or out of iCloud to update the account stauts.
        Debug.Log("Account status notification handlers attached. Sign in or out of iCloud ot update.");
        Debug.Log("Waiting for change in account status...");
    }

    private void AccountStatusChanged(NSNotification obj)
    {
        // There's nothing useful in the NSNotification instance on the objective-c
        // side. Apple now wants you to check the account status seperately using
        // this call...
        // Getting the account status as an asyncronous call, with the potential
        // for an error.
        Debug.Log("CKContainer account status changed notification. Fetching account status...");
        CKContainer.DefaultContainer().AccountStatusWithCompletionHandler(OnAccountStatusComplete);
    }

    private void OnAccountStatusComplete(CKAccountStatus accountStatus, NSError error)
    { 
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            // Since AccoutStatuWithCompletionHandler is invoked everytime the Notification is sent
            // and the notification is sent for many reasons. You may want to check to see that the
            // account status has actually changed...

            // Compare the account status value against a cached version

            if (accountStatus != currentAccountStatus)
            {
                CKAccountStatus? oldAccountStatus = currentAccountStatus;
                currentAccountStatus = accountStatus;
                Debug.Log(string.Format("Account status changed from '{0}' to '{1}'", oldAccountStatus, currentAccountStatus));
            }
        }
    }

    private void OnCloudIdentityChanged(NSNotification obj)
    {
        Debug.Log("OnCloudIdentityChanged");

        // In order to see if the current iCloud identity has changed
        // Check the current token against the last one
        // Tokens are opaque (no public properties). All you can do is compare them

        var NextToken = NSFileManager.DefaultManager().UbiquityIdentityToken;
        if(Token == NextToken)
        {
            Debug.Log("The cloud identity has not changed");
        }
        else
        {
            Debug.Log("The cloud identity has chnaged");
        }

        // If you wanted to unsubscribe from the notification you would do so like..
        CKContainer.RemoveAccountChangedNotificationObserver(Unsubscriber);
    }
}
