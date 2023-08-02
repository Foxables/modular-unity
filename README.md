# Modular Unity

The patterns in use within this project promote maximum decoupling between modules.

## Patterns in use;
- Container Based Dependency Injection
- Service EventBus
- Module Components
- Assembly Based Self-Discovering and Registering Modules.

## Flow;
`Publisher -> EventBus -> Subscriber.Receiver`
1. An arbitrary event is published to the EventBus.
1. The EventBus scans through subscribers that have subscribed to the Type of the arbitrary event.
1. Upon successful identification, the `Receiver` method with the payload as an argument is called against any identified subscribers.
1. The module's `Receiver` method is called and the module executes.

## Notes
- `Assets/Core` houses the Core functionality for this "Framework".
- `Assets/Modules` is where you add your modules.
- All modules must implement the `ModuleInterface`.
- All events must implement the `EventInterface`.
- All Modules and their respective events must be self-contained.
- All Modules must implement the `SubscriberInterface`.
- To Prevent a Module from being initialised, it must not implement `ModuleInterface`.
- The Scene that this 'Framework' should be initialised within must contain the `ModularUnity.prefab` object.
