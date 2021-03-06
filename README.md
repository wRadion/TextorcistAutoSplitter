# The Textorcist AutoSplitter(s)

These are two autosplitters that work together to be used when speedrunning the game.

## Textorcist LiveSplit AutoSplitter (ASL)

This is just a .asl file that can be loaded inside LiveSplit with the _Scriptable AutoSplitter_ component.

### What does it do

It **starts** the timer when you **load a boss** from the location selection menu.

### Configuration

There is one option, 'Single Boss' that, when checked, **resets** the timer when you do **Retry** in the pause menu.

## Textorcist AutoSplitter

This is the C# solution in the repo.

### Requirements

You will need [LiveSplit Server](https://github.com/LiveSplit/LiveSplit.Server) to make this work with your LiveSplit.
Then, Start the server in LiveSplit and Start the AutoSplitter.

### Configuration

You can configure the keys you use to move Ray when Shift is pressed.
By default, it's "ZQSD" (French layout). I will change it to "WASD" and add a save file so you don't have to change everytime you start the program.

### How does it work

The AutoSplitter detects all your key inputs and will split if you type _Amen_. If you move in between typing the word, it will (hopefully) also split. The only edge cases are: if you make a mistake (for example, typing _Amne_ then _en_) it will not work. And if the word is scrambled, it will not work either. Basically if you don't type **exactly** _Amen_ in that order, it will not work. If you speedrun the game, usually you should be able to type _Amen_ in a single burst without failing!

## Example in action

[Here is a run](https://www.twitch.tv/videos/1464964015) I did using both autosplitters (no manual split except for Laurentius climb skip).
