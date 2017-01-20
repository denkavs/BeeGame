Program developed on Visual Studio 2015.

Program contains from two parts.
-Server part includes self-hosted RESTfull web service with one method. Default method path is "http://localhost:9001/api/game". 
	* Server could be run from bin directory with file "BeeGameHost.exe"
	**To create game client calls GET / http://localhost:9001/api/game
	**To Hit any bees client calls POST / http://localhost:9001/api/game/{1} where {1} is game id returned previouse call.
	**Parameter for game could be configured in .config file - "BeeConfig" section. ("bin\BeeGameHost.exe.config")

-Client is simple html file. Client is based on AngularJS framework.
	**First state is Init state of game.
	**Press "Hit" button first time to init game.
	**Every next button click will hit rundom selected bee.
	**Last hitted bee is marked by word "hitted".

