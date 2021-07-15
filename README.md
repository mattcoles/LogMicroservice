# LogMicroservice

Using .Net, develop a microservice exposing an API that writes log messages from external services to a text file. The messages are to include an id, a date and up to 255 characters of text.
<br/>

### The Api

The Api takes a POST request at __api/log__ with the log in the request body.
Api definitions and schemas can be viewd at __/swagger/index.html__  
<br/>  

### The Log

The log message should be sent in the following format.

    {
      "id": "bd5f604d-cbb1-4380-a93b-67852abdeeb5",
      "date": "2021-07-15T13:00:00",
      "message": "The log file message"
    }
    
If using Postman, copy and past the following code into the __Pre-request Script__ tab of the POST

    const moment = require('moment');
    pm.globals.set("today", moment().format("YYYY-MM-DDTHH:MM:SS"));
    
Then use the following to automatically generate the log id and date.

    {
      "id": "{{$guid}}",
      "date": "{{today}}",
      "message": "Another log file message"
    }  
<br/>

### Writing to a file

Once the log has been processed and validated it is written to a text file in __\Logs\logs.txt__<br/>
If validation fails or antoher error occurs the error is wrtten to a file in __\Logs\errors.txt__
