using System;
using System.Collections.Generic;

namespace WPF_OSTATECZNE_STARCIE.Command;

public class Mediator<T> {
    private static readonly Mediator<T> _instance = new Mediator<T>();
    private readonly List<Action<T>>_actions = new List<Action<T>>();

    public static Mediator<T> Instance => _instance;

    public void Subscribe(Action<T> action) {
        _actions.Add(action);
    }

    public void Unsubscribe(Action<T> action) {
        _actions.Remove(action);
    }

    public void Notify(T data) {
        foreach (var action in _actions) {
            action(data);
        }
    }
}