# Mars Rover Challenge

My solution might be a little odd, but I made it with some sort of emulation vibe in mind. I have two martian classes that are supposed to represent the real thing; Mars and Plateau. For example, that's why, when the rover wants to move, it's Plateau the class that resolves the movement. The only class that can communicate with Mars is the rover itself. All the user's inputs are through the class MissionControl.

Another thing I did is not allowing the user to create a Mars plateau. The Mars class itself creates it's plateaus, again in some sort of real life emulation. I allow multiple rovers in multiple plateaus, only in the existing ones inside Mars, as I said.

I did no obstacles although as I intended to at first. No reason other than I want to send this to you as soon as possible, so I can have some feedback. Maybe I'll include them in the future if no homework is assigned. I've made the code quite expandable, so it should be quite simple.

Finally, I must apologize because I forgot to commit until the end of the development. Sorry!

### Class Structure and Encapsulation

The flow of the code is as follows: MissionControl handles all the MarsRover instances. MarsRover talkes with it's Plateau and moves though it. Mars contains all the Plateaus, and lands the rover in one of them, this is what provides MarsRover It's Plateau.

MissionControl also has access to a class named PlateauData, parent of Plateau. It's main use is to connect the real plateaus with the user, so they can access I'ts data but not It's methods.

### Mars Rover Control Shell Usage

Just because it's fun, I've made a functional shell. Is not very detailed, but If you want to try it, just compile MarsRoverControl. Use the "help" coomand to see the list of posible actions. I hope you have fun!