# TO-DO 

This document is a changing list of tasks that I want to address when I have the time.

## Behavior Tree Features

- Add a probability-based component to animal behavior. For example, an animal that is neither hungry nor thirsty may want to choose between several idle behaviors, such as resting in place, wandering, or looking for companionship from members of the same species.
- Generate the behavior tree UI dynamically, such that it acccommodates changes of the behavior trees at runtime.
- Incorporate some evolutionary aspect into the behavior trees. This can be on both an individual level, where an animal’s behavior adapts over its lifespan, or on a generational level, where evolution selects the animals with the best-surviving behavior trees.

## Environment Features

- Add sources of water to the environment.
- Add trees in the environment for acorns to fall from.
- Add decorative assets and a prettier sky background.
- Add a day / night cycle and weather.

## Animal Features

- Add behavior that seeks out water sources to periodically drink from.
	- This will require pathfinding to also be implemented.
- Add additional animal species to the food chain and their food sources. Ideally, there would be herbivorous animals that are preyed upon by intermediary predators, which in turn can be eaten by the largest predators in that environment. For example,
	- Rabbits, which can feed on plants that grow in the environment
	- Bears, which can eat smaller animals as well as berries from plants
- Add reproductive behavior so that animal populations don't die out.
- Add lifespan variables to the animals so they can die of natural causes.
- Add a more realistic "look around" behavior. (Currently the animal's field of view is unnaturally wide to compensate for the lack of this.)
- Add animations.
- Rework some of the less-convincing behaviors:
	- Fix wonky collision-avoiding behavior, which causes agents to wiggle towards their destinations.
	- Add more intelligence to the running-away behavior, which only makes agents run in the exact opposite direction of the pursuing predator.

## UI Features

- Add user interaction, such that the user can influence interactions between animals and their environments, such as dragging a prey away from a predator (or vice versa) or helping an herbivore find a plant that it eats.
- Fix camera controls, which are currently too constrained.
- Polish the UI, both for aesthetics and for easier use.