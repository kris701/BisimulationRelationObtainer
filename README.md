# Bisimulation Relation Obtainer
This is a small program to get the bisimulation relations between two processes in DFA format.
It currently have two methods to find these relations:
* A Naive approach
* The Hopcroft and Karp approach
* The Pous And Bonchi Optimization

The program has a CLI interface, that accepts DFA's in file format.

![image](https://user-images.githubusercontent.com/22596587/228923242-47399694-31fe-4531-9467-38ef9acf8b6c.png)
![image](https://user-images.githubusercontent.com/22596587/228923348-dda8d1c8-47fe-427c-b034-297d9a418251.png)

## CLI Arguments
There are the following command line arguments:
* `--pprocess`: The path to the one process file
* `--qprocess`: The path to the other process file
* `--obtainer`: What obtainer to use

## File Format
The input file format for this program is very simple, and consists of 3 parts:
* Label declaration
* State declaration
* Transitions

The label declarations is simply a set of what labels are available in the process:
* `{a, b, c, ...}`

The state declarations consists of max three parts and minimum one:
* `[(StateName):IsInit:IsFinal]`
The `IsInit` and `IsFinal` is optional.

Lastly, there is the transitions. These describe how to jump from state to state through a label:
* `(StateName) LabelName (StateName)`

![image](https://user-images.githubusercontent.com/22596587/231118394-e9988019-cb56-462b-aa55-fc922ba4386e.png)
