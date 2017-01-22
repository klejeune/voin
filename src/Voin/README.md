# Voin

Voin is a C# user right management system.

## Why do you need it?
Classic user right management in complex applications have several problems:
- apart from the definition of the rules, most of the code is technical, hence without business value
- in a lot of applications, access rules are all over the place, embedded in database queries
- it's usually efficient to check if a particular user has access to a resource, but not to list all the users having access to it
- access rules are usually not easily understandable from code, and difficult to maintain

Voin addresses all these problems:
- the access right engine is separated from your core code, except for the access rules definition
- access rules are easily readable and easy to modify
- access rights are stored in a database of your choosing (or can be computed in-memory at the startup of your app)

## How does it work?
You can have a look at the test project for full code examples.

Your users (or web clients, or whatever must have a controlled access) must be represented by a class inheriting `IActor`. You
	can make you own classes implement this, or create a dedicated class.

Your resources (whatever must be protected from unauthorized actors) must be represented by a class inheriting `IResource`.
	You can make you own classes implement this, or create a dedicated class.

Your access rights ("read", "write", "see", "delete", "modify", "update some property", "like", etc.) must be represented by a class inheriting `IRight`.

You can instanciate the RightService like this:
```C#
// Actors
var alice = new User("Alice", "alice@example.com");
var bob = new User("Bob", "");

// Rights
var see = new Right("see");
var use = new Right("use");

// Resources
var redPrinter = new Printer("Red Printer");
var pinkPrinter = new Printer("Pink Printer");
var purplePrinter = new Printer("Purple Printer");

// We create the RightService
var rightService = new RightService(
	new InMemoryRightStore(), 
	new InMemoryRepository<IActor>(new[] { alice, bob }),
	new InMemoryRepository<IResource>(new IResource[] { redPrinter, pinkPrinter }), 
	new InMemoryRepository<IRight>(new[] { see, use }));

// We use an in-memory right store (InMemoryRightStore), so we add rules now, and they will be computed. 
// If we had used a persisted right store, the already computed rules would have been passed as an additional parameter to the constructor.
rightService.AddRule(_ => _.AnyOne().Can(see).The(redPrinter));
rightService.AddRule(_ => _.All<User>().With(u => !string.IsNullOrWhiteSpace(u.Email)).Can(see).The(pinkPrinter));
rightService.AddRule(_ => _.Actor(alice).Can(see).The(purplePrinter));
```

The `AddRule` method will compute the rule for the existing users and resources, and store it in the right store (here, an `InMemoryRightStore`).

In order to check if a user has some access right, you can do:
```C#
var aliceCanSeeTheRedPrinter = rightService.HasRight(alice, see, redPrinter);
```

You can also query which resources a user can see, and similar information:
```C#
var whoCanSeeTheRedPrinter = rightService.GetAuthorizedActors(see, redPrinter);
var whatCanAliceSee = rightService.GetAccessibleResources(alice, see);
var whatRightsHasAliceOnTheRedPrinter = rightService.GetRights(alice, redPrinter);
```

## Why "Voin"?
"Voin" means "I can" in estonian.

## Upcoming features
* Sync methods
* More complex tests (groups, delegates...)
* Store the already computed rules in some way so that a sanity check can be performed at the `RightService` startup
* Nuget package
* Adapt the english-like DSL for `HasRight`
* Add a full english DSL (`rightService.AddRule("Anyone can see the right printer.");`)