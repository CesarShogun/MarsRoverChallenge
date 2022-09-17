using System;

namespace MarsRoverControl.Models
{
    public struct MarsEvent
    {
        public Event Event;
        public Coords Coords;
    }

    public enum Event
    {
        NoEvent,
        Obstacle,
        Edge
    }
}
