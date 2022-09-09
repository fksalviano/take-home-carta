# Take Home - Carta

Take Home test developed in **C#** with **.NET Core 6.0**.

Using Clean Architecture and Vertical Slice, isolating the Application Common Domain from Use Case Domain.

### Use Cases

- **GetVested**: 
    Responsible to get Vested Schedules based on CSV file content mapped and the input Target Date.
    
### Worker

The console application Program class acts as a Worker that executes the Use Case.

### Dependency Injection

The project uses Dependence Injection to build the container services and uses self-installers to add each Use Case on the container independently.

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
    
- Moq.AutoMock:
    https://www.nuget.org/packages/moq.automock
