using _Scripts.Services.EventsService;
using UnityEngine;

namespace _Scripts.Handlers
{
    public class FpsHandler
    {

        private FpsHandler(ILogEventsService logEventsService)
        {
            logEventsService.LogEvents("Set_current_FPS");
            Application.targetFrameRate = 60;
        }
    }
}