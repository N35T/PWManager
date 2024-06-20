namespace PWManager.UI.Extensions {
    public static class EventExtensions {
        public static void SafeTrigger<TEventArgs>
                (this EventHandler<TEventArgs> eventToTrigger,
                object? sender, TEventArgs eventArgs)
                where TEventArgs : EventArgs {
            eventToTrigger?.Invoke(sender, eventArgs);
        }

        public static void SafeTrigger<TEventArgs>
                (this Action<TEventArgs> eventToTrigger, TEventArgs eventArgs)
                where TEventArgs : EventArgs {
            eventToTrigger?.Invoke(eventArgs);
        }

        public static void SafeTrigger(this Action eventToTrigger) {
            eventToTrigger?.Invoke();
        }
    }
}
