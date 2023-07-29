using Core.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers;

[Area("Admin")]
public class BaseMvcController : Controller
{
    protected ILogger _logger;


    #region Notificatoin

    protected void ShowMessage(string message, MessageType messageType, int durationSecond = 5, bool showCloseButton = true, bool showProgressBar = true)
    {
        TempData["Message"] = message;
        TempData["MessageType"] = messageType;
        TempData["DurationSecond"] = durationSecond;
        TempData["ShowCloseButton"] = showCloseButton ? "true" : "false";
        TempData["ShowProgressBar"] = showProgressBar ? "true" : "false";

        switch (messageType)
        {
            case MessageType.Success:
                TempData["MessageTitle"] = "Success";
                break;
            case MessageType.Error:
                TempData["MessageTitle"] = "Error";
                break;
            case MessageType.Warning:
                TempData["MessageTitle"] = "Warning";
                break;
            case MessageType.Info:
                TempData["MessageTitle"] = "Info";
                break;
            default:
                TempData["MessageTitle"] = "Message";
                break;
        }
    }
    

    #endregion

    #region MVC Error Message
    protected static string GetAllErrorMessage(ModelStateDictionary modelState)
    {
        var message = string.Empty;
        foreach (var state in modelState)
        {
            foreach (var error in state.Value.Errors)
            {
                message += error.ErrorMessage + Environment.NewLine;
            }
        }

        return message;
    }
    
    protected static string GetErrorMessage(ModelStateDictionary modelState)
    {
        var message = modelState.Values.SelectMany(v => v.Errors).FirstOrDefault()!.ErrorMessage;
        
        return message;
    }
    #endregion
}