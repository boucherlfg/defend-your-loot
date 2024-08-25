public class BaseEvent {
    private event System.Action Changed;

    public void Invoke() {
        Changed?.Invoke();
    }

    public void Subscribe(System.Action callback) {
        Changed += callback;
    }
    public void Unsubscribe(System.Action callback) {
        Changed -= callback;
    }
}

public class BaseEvent<T> {
    private event System.Action<T> Changed;

    public void Invoke(T value) {
        Changed?.Invoke(value);
    }

    public void Subscribe(System.Action<T> callback) {
        Changed += callback;
    }
    public void Unsubscribe(System.Action<T> callback) {
        Changed -= callback;
    }
}