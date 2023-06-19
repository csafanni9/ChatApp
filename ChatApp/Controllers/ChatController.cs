using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatApp.Models;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        public async Task<ActionResult> Index(String question, String[] history)
        {
            String[] answer = { "", "" };
            int answerInd = 1;

            if (history != null)
            {
                history = history.Append("").ToArray();
                history = history.Append("").ToArray();
                answerInd = history.Length - 1;
                answer = history;
            }

            if (question != null)
            {
                answer[answerInd - 1] = question;
                answer[answerInd] = await ChatModel.GenerateText(question);
            }

            return View(answer);
        }
    }
}