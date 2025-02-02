# Modular Unity Framework

A modern, event-driven framework for building maintainable and scalable Unity games.

## What is This?

This framework provides a structured way to build Unity games where different parts of your game can work together without being directly dependent on each other. Think of it like building with LEGO blocks - each piece is separate but can connect with others in a standard way.

## Why Use This Framework?

### Common Problems in Unity Projects This Solves

1. **"Spaghetti Code"** - In many Unity projects, scripts directly reference each other, creating a tangled mess of dependencies. When you change one thing, unexpected issues pop up elsewhere.

2. **Difficult Team Collaboration** - Traditional Unity projects often have team members stepping on each other's toes because game systems are tightly coupled.

3. **Code Reuse Challenges** - Copying features between projects usually means copying a lot of related code because everything is connected.

4. **Testing Difficulties** - When components are tightly coupled, it's hard to test them in isolation.

### How This Framework Helps

1. **Message-Based Communication**
   - Instead of scripts directly calling each other, they communicate through events
   - Like sending a letter instead of walking to someone's house - the sender doesn't need to know where the receiver lives

2. **Self-Contained Modules**
   - Each feature (like player movement, inventory, or UI) lives in its own module
   - Modules can be added or removed without breaking other parts of the game
   - Like plug-and-play electronics - each device works independently

3. **Automated Setup**
   - The framework automatically discovers and connects modules
   - No need to manually wire everything together in the Unity Inspector

## Key Features

### 1. Event System
- Type-safe events ensure you can't send the wrong data
- Events can carry specific data payloads
- Multiple systems can listen for the same event without knowing about each other

### 2. Module System
- Each module is self-contained with its own:
  - Code
  - Unity prefabs
  - Event types
- Modules can be enabled/disabled using a simple text file
- Easy to move modules between projects

### 3. Object Management
- Centralized system for creating and destroying game objects
- Handles parent-child relationships automatically
- Keeps track of all created objects for easy cleanup

### 4. UI Integration
- Dedicated modules for UI management
- Separates game logic from presentation
- Supports both in-game UI and menus

## Getting Started

1. Add the `ModularUnity.prefab` to your first scene
2. Create a new module in the `Assets/Modules` folder
3. Implement the `ModuleInterface`
4. That's it! The framework will automatically find and initialize your module

## Trade-offs and Considerations

### Advantages
1. **Maintainability**
   - Easy to understand what each part does
   - Changes in one module don't break others
   - Clear structure for new team members

2. **Scalability**
   - Add new features without changing existing code
   - Multiple teams can work independently
   - Easy to test individual components

3. **Reusability**
   - Copy entire features between projects
   - Share modules with the community
   - No hidden dependencies

### Potential Drawbacks
1. **Learning Curve**
   - New pattern to learn for team members
   - Different from traditional Unity development
   - Requires understanding of events and modules

2. **Initial Setup Time**
   - More upfront planning needed
   - Need to create proper event types
   - Module structure needs thought

3. **Performance Considerations**
   - Event system has slight overhead
   - More memory used for event management
   - Not ideal for extremely performance-critical games

## When to Use This Framework

### Good Fit For:
- Medium to large games
- Team-based development
- Games that will need ongoing updates
- Projects that need clear architecture
- Games with many interacting systems

### Maybe Not For:
- Small, simple games
- Quick prototypes
- Highly performance-critical games
- Solo developers who prefer direct coupling

## Example: Player Movement

Traditional Unity approach:
```csharp
// Direct reference to player
public PlayerController player;

void Update() {
    player.Move(new Vector3(1,0,0));
}
```

This framework's approach:
```csharp
// Send movement event
Publisher.Dispatch(new PlayerMoveEvent(new Vector3(1,0,0)));
```

The second approach means:
- The sender doesn't need to know about the player
- Multiple things can respond to movement
- Easy to change how movement works without touching other code

## Best Practices

1. Keep modules focused on one feature
2. Use meaningful event names and payloads
3. Don't bypass the event system
4. Document your module's events
5. Use the .ignoremodule file for testing

## Support and Community

- Report issues on GitHub
- Share modules with other developers
- Contribute improvements to the core framework

## License

Apache 2.0 License for Open Source use and distribution. It is permitted to use this framework in commercial projects without royalty.

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