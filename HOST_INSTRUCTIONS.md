# Instructions to assist the workshop runner in keeping the workshop on track

## Setup Instructions

1. Have the attendees clone the repo, and checkout tag: `unsolved`

1. Have the workshop attendees set up an environment variable for:  
   `ASPNETCORE_ENVIRONMENT=development`

1. Have the workshop attendees set up an environment variable or secret store for:  
   `WeatherApiKey=<your weather api key>`

1. Have the attendees run the console app to see that it works  
   `dotnet restore`
   `cd TheStillHeron.TestWorkshop.Console`  
   `dotnet run`

## Exercise One instructions

1. Have the attendees Ctrl+Shift+F for "e_x\_._1" (without the underscores)

1. Draw the attendees attention to the test, and the fact that it doesn't pass (reliably)

1. Talk the attendees through writing and passing an interface for the api

1. Talk the attendees through creating a mock (using moq) for the api  
   Ctrl+Shift+F for snippet\_._1 for a payload

---

Helpful snippets you may wish to provide the class:

_Run only the relevant test_

```
dotnet test --filter When_Rain_Is_Predicted_Umbrella_Is_Recommended
```

_Construct the mock that will be required_

```
var mock = new Mock<IWeatherApiClient>();
mock.Setup(x => x.GetCurrentWeather()).Returns(Task.FromResul(response));
var apiClient = mock.Object;
```

## Exercise Two instructions

1. Have the attendees Ctrl+Shift+F for "e_x\_._2" (without the underscores)

1. Draw the attendees attention to the test, and the fact that they cannot control whether it will pass because of the lack of determinism with system date times

1. Talk the attendees through writing an abstraction to wrap System.DateTime

1. Talk the attendees through providing that abstraction to the DayPlan() and IsDue() functions

1. Talk the attendees through creating a mock for the abstraction to use in the test

---

Helpful snippets you may wish to provide the class:

_Run only the relevant test_

```
dotnet test --filter When_Chore_Is_Due_It_Is_Listed_In_The_Plan
```

## Exercise Three instructions

1. Have the attendees Ctrl+Shift+F for "e_x\_._1" (without the underscores)

1. Draw the attendees attention to the test, and the fact that we can't get the match to confirm the outcome of the test

1. Talk the attendees through refactoring the `get` endpoint into a command so it can be tested end-to-end easily

---

Helpful snippets you may wish to provide the class:

_Run only the relevant test_

```
dotnet test --filter Can_Add_Match_To_Season
```