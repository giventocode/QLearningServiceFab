

### Instructions
1.- Install Service Fabric SDK and Visual Studio 2015. More information on how to set up your environment [here](https://azure.microsoft.com/en-us/documentation/articles/service-fabric-get-started)
</br>
</br>

2.- Publish the QLearningAPI and QLearningServiceFab apps to a Service Fabric cluster.
</br>
</br>

3.- Call the QTrainer API to start the training process for all the initial transitions. 1 to 9 for the game of tic-tac-toe.
</br>
</br>
  http://CLUSTER:PORT/api/qtrainer/start/1
</br>
  ...
</br>
  http://CLUSTER:PORT/api/qtrainer/start/9
  </br>
  </br>
  *Note* The training might take more than 10 minutes to complete. 
</br>
</br>
4.- Configure the client app to point to your QTrainer API. In App.cs locate and modify the following with your server and port information:
</br>
<p>
<code>
      public static Uri QServiceUrl = new Uri("http://CLUSTER:PORT/api/qtrainer/nextvalue/");
</code>        
</p>
