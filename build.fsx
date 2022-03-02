#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Testing.MSpec 
nuget Fake.DotNet.MSBuild 
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Testing.XUnit2
nuget xunit.runner.console
nuget Fake.Core.Target //"

#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.DotNet.Testing
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

let testDir ="FiveCardPoker.Test/**/bin/Release/net5.0/"

Target.initEnvironment ()
 
Target.create "Clean_Library" (fun _ ->
    !! "FiveCardPoker.Lib/**/bin"
    ++ "FiveCardPoker.Lib/**/obj"
    |> Shell.cleanDirs 
)

Target.create "Clean_Web_APP" (fun _ ->
   !! "FiveCardDraw.Web/**/bin"
    ++ "FiveCardDraw.Web/**/obj"
    |> Shell.cleanDirs 
)

Target.create "Clean_Test_Project" (fun _ ->
    !! "FiveCardDraw.Test/**/bin"
    ++ "FiveCardDraw.Test/**/obj"
    |> Shell.cleanDirs 
)

Target.create "Build_Library" (fun _ ->
    !! "FiveCardPoker.Lib/**/*.*proj" 
    |> Seq.iter (DotNet.build id)
)

Target.create "Build_Web_APP" (fun _ -> 
    !! "FiveCardDraw.Web/**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

Target.create "Build_Tests" (fun _ ->
    !! "FiveCardDraw.Test/**/*.*proj"
    |> Seq.iter (DotNet.build id)
    
)

Target.create "All" ignore
"Clean_Library"
  ==> "Clean_Web_APP"
  ==> "Clean_Test_Project"
  ==> "Build_Library"
  ==> "Build_Web_APP"
  ==> "Build_Tests"
  ==> "All"

Target.runOrDefault "All"
