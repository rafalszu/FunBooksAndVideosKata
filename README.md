# FunBooksAndVideos Kata #

### what is it about? ###
The solution tries to solve a variation of one the codekatas available using .net core 2.0. Presented approach is rather simplified due time constraints and is not production ready. 

### different approach ###
Another approach, which could result in a serious perf. boost, would be using CQRS and event sourcing. This seems like a perfect fit for the given scenario, as orders would be processed in the background asynchronously by workers subscribed to dedicated messaging channels.

As mentioned, I decided to go with the simpler approach due to serious time limitations and the easibility of unit testing the code. As much as I like event sourcing and think that this is the way to go for proper scalable backends, the second approach would require far more complex test setup as we would need to mock all pub/sub messages.

### how to run ###
* install .net core 2.0 if missing
* clone this repo to a destination of your choice
* start up the console
* navigate to the location where you cloned the repo 
* restore all dependencies
    ~~~
    dotnet restore
    ~~~
* run unit tests
    ~~~
    dotnet test
    ~~~
