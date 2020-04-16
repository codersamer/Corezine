using Corezine.Services.Enumrations;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
namespace Corezine.Services.Contracts
{
    public interface IFeedbackService
    {

        IEnumerable<String> MessagesFeedback { get; }
        IEnumerable<String> InfoFeedback { get; }
        IEnumerable<String> SuccessFeedback { get; }
        IEnumerable<String> WarningFeedback { get; }
        IEnumerable<String> ErrorFeedback { get; }
        IEnumerable<String> DebugFeedback { get; }


        void Add(FeedbackType type, String message);
        void AddMessage(String message);
        void AddInfo(String message);
        void AddSuccess(String message);
        void AddWarning(String message);
        void AddError(String message);
        void AddDebug(String message);


        HtmlString Show(FeedbackType type = FeedbackType.Error);
        HtmlString ShowMessage();
        HtmlString ShowInfo();
        HtmlString ShowSuccess();
        HtmlString ShowWarning();
        HtmlString ShowError();
        HtmlString ShowDebug();
        HtmlString ShowAll();

        IEnumerable<String> Get(FeedbackType type = FeedbackType.Error);


    }
    

}
