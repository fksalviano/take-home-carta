# Take Home - Carta

Take Home test developed in **C#** with **.NET Core 6.0**.

Using Clean Architecture and Vertical Slice, isolating the Domain of each Use Case. 

### Use Cases

To better exemplify the use of this architecture, the Application Domain was separated on the following use cases:

- **ReadFile**: 
    Responsable to read the CSV file and return it´s content mapped in a list of Domain objects.

- **GetVested**: 
    Responsable to get Vested Schedules based on CSV file content that was mapped and the input Target Date.
    
### Worker

The console application uses a Worker class to orchestrate the Use Cases execution.

### Dependency Injection

The Worker class calls the Use Cases based on interfaces that are injecteds by Dependence Injection.

## Configuration

### Requirements

Need to install the follow:

- Git:
    https://git-scm.com/downloads

- Dotnet Core 6.0 SDK and Runtime:
    https://dotnet.microsoft.com/en-us/download/dotnet/6.0
    

## Getting Started

#### Clone the repository (skip this step if already downloaded the project):

```bash
git clone https://github.com/fksalviano/take-home-carta.git
```

#### Go to the project directory

```bash
cd take-home-carta
```

#### Build the project

```bash
dotnet build
```

#### Run (using dotnet)

```bash
dotnet run --project src/Vesting/Worker test.csv 2022-01-01 1
```

#### Run (using bash)

```bash
./vesting_program test.csv 2022-01-01 1
```

#### PS: There is a **test.csv** file on the root folder to run.

## Packages

The project uses the following packages

- FluentValidation:
    https://www.nuget.org/packages/fluentvalidation

- FluentAssertions:
    https://www.nuget.org/packages/fluentassertions
    
- XUnit:
    https://www.nuget.org/packages/xunit
    
- AutoFixture:
    https://www.nuget.org/packages/autofixture
