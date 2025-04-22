using Azure;
using Azure.Communication.Email;

string connectionString = "input your connection string here";
var emailClient = new EmailClient(connectionString);
var emailRecipients = new EmailRecipients(
[
    new("davidtan@greendotcorp.com"),
    new("tony.liu@greendotcorp.com")
]);

var emailContent = new EmailContent("Additional Card Inventory Request for Owens and Minor Locations")
{
    Html = File.ReadAllText("EmailTemplate.html")
};

var emailSendOperation = await emailClient.SendAsync(
    WaitUntil.Completed,
    new EmailMessage(
        "DoNotReply@62bd4d94-ac36-403d-8b1d-d1399285c29e.azurecomm.net",
        recipients:emailRecipients,
        content: emailContent)
);