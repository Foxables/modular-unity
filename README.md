# Modular Unity

The patterns in use within this project promote maximum decoupling between modules.

## Patterns in use;
- Container Based Dependency Injection
- Pub/Sub EventBus
- Module Based Components
- Assembly Based Discovery and Instantiation.

## Flow;
`Publisher -> EventBus -> Subscriber.Receiver`
1. An arbitrary event is published to the EventBus using the Dispatch method of a Publisher.
1. The EventBus obtains all subscribers that have subscribed to the Type of arbitrary event sent.
1. Upon successful identification, the registered callback method of the subscriber is called with the event object as its argument for each subscriber.
    - The `Receiver` method is used by the `FactoryCreateAndListen` method when auto-loading and subscribing modules during start up.

## Notes
- `Assets/Core` houses the Core functionality for this "Framework".
- `Assets/Modules` is where you add your modules.
- `Assets/Resources/Modules` is where you add your companion prefab files that modules can instantiate into the game, this means the `.prefab` files for UI Collections and any other resource that is loaded through ResourceLoad that belongs to a module.
- All modules must implement the `ModuleInterface`.
- All events must implement the `EventInterface`.
- All Modules and their respective events must be self-contained within the file-system, enabling copy-and-paste of the module between projects.
- To Prevent a Module from being initialised, it must not implement `ModuleInterface` and must be added to the `.ignoremodule` file.
- The Scene that this Framework should be initialised within must contain the `ModularUnity.prefab` object.
- Objects created through the `ObjectManagementModule` must be deleted through the `ObjectManagementModule` by dispatching an `ObjectDestroyEvent`.
- Modules must never be called directly, they must only use events.

Note: Currently, modules can be tightly coupled to events. This will be changed.