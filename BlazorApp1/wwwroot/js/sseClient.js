window.sseInterop = {
    startSse: function (url, dotNetRef) {
        const eventSource = new EventSource(url);

        eventSource.onmessage = function (event) {
            dotNetRef.invokeMethodAsync('OnSseMessage', event.data);
        };

        eventSource.onerror = function (err) {
            console.error("SSE Error:", err);
            eventSource.close();
        };
    }
};
