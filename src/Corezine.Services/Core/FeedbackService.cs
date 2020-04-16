using Corezine.Services.Contracts;
using Corezine.Services.Entities;
using Corezine.Services.Enumrations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Corezine.Services.Core
{
    public class FeedbackService : IFeedbackService
    {
        

        public IEnumerable<string> MessagesFeedback => Get(FeedbackType.Message);

        public IEnumerable<string> InfoFeedback => Get(FeedbackType.Info);

        public IEnumerable<string> SuccessFeedback => Get(FeedbackType.Success);

        public IEnumerable<string> WarningFeedback => Get(FeedbackType.Warning);

        public IEnumerable<string> ErrorFeedback => Get(FeedbackType.Error);

        public IEnumerable<string> DebugFeedback => Get(FeedbackType.Debug);

        public static HttpContext Context { get; set; }

        protected static FeedbackEntity CurrentFeedback = new FeedbackEntity();

        protected static FeedbackEntity OldFeedback = new FeedbackEntity();

        public const String FEEDBACK_SESSION_KEY = "SESSION_FEEDBACK";

        public FeedbackService()
        {
            String SerializedFeedback = Context.Session.GetString(FEEDBACK_SESSION_KEY);
            if (!String.IsNullOrEmpty(SerializedFeedback))
            { 
                OldFeedback = JsonSerializer.Deserialize<FeedbackEntity>(SerializedFeedback);
                Context.Session.SetString(FEEDBACK_SESSION_KEY, "");
            }
            else { OldFeedback = new FeedbackEntity(); }
            CurrentFeedback = new FeedbackEntity();
        }


        public void Add(FeedbackType type, string message)
        {
            switch (type)
            {
                case FeedbackType.Debug: CurrentFeedback.DebugRecords.Add(message); break;
                case FeedbackType.Error: CurrentFeedback.ErrorRecords.Add(message); break;
                case FeedbackType.Info: CurrentFeedback.InfoRecords.Add(message); break;
                case FeedbackType.Message: CurrentFeedback.MessageRecords.Add(message); break;
                case FeedbackType.Success: CurrentFeedback.SuccessRecords.Add(message); break;
                case FeedbackType.Warning: CurrentFeedback.WarningRecords.Add(message); break;
            }
            UpdateSession();
        }

        public IEnumerable<string> Get(FeedbackType type = FeedbackType.Error)
        {
            List<String> Data = new List<String>();
            switch(type)
            {
                case FeedbackType.Debug: Data = OldFeedback.DebugRecords;break;
                case FeedbackType.Error: Data = OldFeedback.ErrorRecords; break;
                case FeedbackType.Info: Data = OldFeedback.InfoRecords; break;
                case FeedbackType.Message: Data = OldFeedback.MessageRecords; break;
                case FeedbackType.Success: Data = OldFeedback.SuccessRecords; break;
                case FeedbackType.Warning: Data = OldFeedback.WarningRecords; break;
            }
            return Data;
        }

        void UpdateSession()
        {
            String JsonData = JsonSerializer.Serialize(CurrentFeedback);
            Context.Session.SetString(FEEDBACK_SESSION_KEY, JsonData);
        }

        public void AddMessage(string message)
        {
            Add(FeedbackType.Message, message);
        }

        public void AddInfo(string message)
        {
            Add(FeedbackType.Info, message);
        }

        public void AddSuccess(string message)
        {
            Add(FeedbackType.Success, message);
        }

        public void AddWarning(string message)
        {
            Add(FeedbackType.Warning, message);
        }

        public void AddError(string message)
        {
            Add(FeedbackType.Error, message);
        }

        public void AddDebug(string message)
        {
            Add(FeedbackType.Debug, message);
        }

        public HtmlString ShowMessage()
        {
            return Show(FeedbackType.Message);
        }

        public HtmlString ShowInfo()
        {
            return Show(FeedbackType.Info);
        }

        public HtmlString ShowSuccess()
        {
            return Show(FeedbackType.Success);
        }

        public HtmlString ShowWarning()
        {
            return Show(FeedbackType.Warning);
        }

        public HtmlString ShowError()
        {
            return Show(FeedbackType.Error);
        }

        public HtmlString ShowDebug()
        {
            return Show(FeedbackType.Debug);
        }

        public HtmlString Show(FeedbackType type = FeedbackType.Error)
        {
            String classes = "is-light ";
            IEnumerable<String> Items = Get(type);
            switch(type)
            {
                case FeedbackType.Debug:    classes = "is-light";  break;
                case FeedbackType.Error:    classes = "is-danger";  break;
                case FeedbackType.Info:     classes = "is-dark";  break;
                case FeedbackType.Message:  classes = "is-info";  break;
                case FeedbackType.Success:  classes = "is-success";  break;
                case FeedbackType.Warning:  classes = "is-warning";  break;
            }
            String Markup = "";
            foreach(String item in Items)
            {
                Markup += $"<div class=\"notification {classes}\">{item}</div>{Environment.NewLine}";
            }
            return new HtmlString(Markup);
        }

        public HtmlString ShowAll()
        {
            HtmlString html = new HtmlString("");
            foreach(FeedbackType type in Enum.GetValues(typeof(FeedbackType)))
            {
                html = new HtmlString(html.Value + Show(type).Value);
            }
            return html;
        }
    }
}
