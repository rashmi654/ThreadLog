
# Logger

This application provides a way to write into a log file using multiple threads. 
The log file name is hardcoded to the  filepath `/log/out.txt`. 
Line count, thread number and current time stamp are written into the file in the format: "<line_count>, <thread_id>, <current_time_stamp>".
Log file contains an initial entry as "0, 0, <current_time_stamp>".

Ten threads are ran simultaneously and each thread writes into the file ten times. 
The application waits for all the threads to run to completion. It then waits for any key press before terminating.
Once the application terminates, the content of the log can be viewed in `/log/out.txt`. 

## FileWriter
FileWriter creates the file in the provided filepath and writes the log into the file
in the format mentioned above. If the folder doesn't exists it creates the folder. 
If the file already exists, it overwrites the file during initialization. 
`WriteLine()` is synchronised and multiple threads can call it at the same time.

## WriterThread
WriterThread creates a new thread and uses the supplied `IFileWriter` to write into the file ten times. 
Any error occurred during thread execution can be retrieved using defined `Exception` property.
The use of this class can be easily replaced by `Task` class.

## Building and Running
Logger can be built in Visual Studio and from the docker image.
### Visual Studio:
Open `Logger.sln` in Visual Studio, build and run the Logger application.
	
### Docker image:
To build the docker image:  

```docker build -t logger:latest .```

To run the application using the docker image

```docker run -i -v c:\junk:/log logger:latest```

 The log file will be saved in local folder `c:\junk`.


A prebuilt docker image is avaiable in docker hub at  `rashmi654/logger:latest`. 
To run the app using the prebuilt image, run

```docker run -i -v c:\junk:/log rashmi654/logger:latest```

Make sure to login into docker hub first using `docker login`.





