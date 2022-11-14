# VPSDemo

The goal of the test task is to develop a web API of a task planning system. The web API must allow to
create and edit a task hierarchy, as well as perform calculations on the data fields of the tasks in the
hierarchy.

### Technologies used:
ASP.NET Core 6, C#, EntityFramework Core, MS SQL Server Database

### Type of architecture used with different layers

![promisechains](https://github.com/domkris/files/blob/master/VPSDemo/cleanarch.png?raw=true)

### Capabilities of the Web API
1. Create Task, Delete Task, Edit Task, Get Task, Assign Sub task
2. Get Task - also retrieves sub Tasks
3. Delete Task - Only a task which does not have sub-tasks can be deleted.

<hr>

## Web API Endpoints & Examples

![promisechains](https://github.com/domkris/files/blob/master/VPSDemo/webapi_1.png?raw=true)

## Create
#### POST VPSDemo_URL/tasks

<table>
<tr>
<th> Request body </th>
<th> Response (SUCCESS 200) </th>
</tr>
<tr>
<td>

```json
{
    "title": "MAIN TASK A",
    "description": "description",
    "effortEstimation": 1
}
```

</td>
<td>

```json
{
    "identifier": 1,
    "title": "MAIN TASK A",
    "description": "description",
    "effortEstimation": 1,
    "aggregatedEffortEstimation": 281,
    "status": "New",
    "subTasks": [],
    "creationDate": "2022-11-13T23:15:12"
}
```

</td>
</tr>
</table>


<table>
<tr>
<th> Request body </th>
<th> Response (FAIL) </th>
</tr>
<tr>
<td>

```json
{ 
  "description": "description"
}
```

</td>
<td>

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Validation Error",
    "status": 400,
    "traceId": "00-21790aa1386552307b4397420650fefa-18df5db6c8db92e1-00",
    "errorMessage": [
        "The Title field is required."
    ]
}
```

</td>
</tr>
</table>

## Edit
#### PUT VPSDemo_URL/tasks/2


<table>
<tr>
<th> Request body </th>
<th> Response (FAIL) </th>
</tr>
<tr>
<td>

```json
{ 
  "description": "description"
}
```

</td>
<td>

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Validation Error",
    "status": 400,
    "traceId": "00-21790aa1386552307b4397420650fefa-18df5db6c8db92e1-00",
    "errorMessage": [
        "The Status field is required.",
        "The field Status is invalid"
    ]
}
```

</td>
</tr>
</table>

## Assign sub task
#### PUT VPSDemo_URL/tasks/assignSubTask/4?parentIdentifier=3

<table>
<tr>
<th> Query (Key: Value) </th>
<th> Response (Success) </th>
</tr>
<tr>
<td>

```javascript

  parentIdentifier: 3

```

</td>
<td>

```json
{
  "identifier": 3,
  "title": "TASK B",
  "description": "description",
  "effortEstimation": 0,
  "aggregatedEffortEstimation": 80,
  "status": "New",
  "subTasks": [
    {
      "identifier": 4,
      "title": "TASK C",
      "description": "description",
      "effortEstimation": 80,
      "aggregatedEffortEstimation": 0,
      "status": "New",
      "subTasks": [],
      "creationDate": "2022-11-13T23:17:44"
    }
  ],
  "creationDate": "2022-11-13T23:15:12"
}
```

</td>
</tr>
</table>

## Fetch 
#### Get VPSDemo_URL/tasks/3

<table>
<tr>
<th> Response (Success) </th>
</tr>
<tr>
<td>

```json
{
  "identifier": 3,
  "title": "TASK B",
  "description": "description",
  "effortEstimation": 0,
  "aggregatedEffortEstimation": 80,
  "status": "New",
  "subTasks": [
    {
      "identifier": 4,
      "title": "TASK C",
      "description": "description",
      "effortEstimation": 80,
      "aggregatedEffortEstimation": 0,
      "status": "New",
      "subTasks": [],
      "creationDate": "2022-11-13T23:17:44"
    }
  ],
  "creationDate": "2022-11-13T23:15:12"
}
```

</td>
</tr>
</table>


## Delete 
#### Delete VPSDemo_URL/tasks/3

<table>
<tr>
<th> Response (Success 204 NoContent) </th>
</tr>
<tr>
<td>


</td>
</tr>
</table>

#### Delete VPSDemo_URL/tasks/1

<table>
<tr>
<th> Response (FAIL) </th>
</tr>
<tr>
<td>

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
    "title": "Conflict",
    "status": 409,
    "traceId": "00-7d2127bfe809238b8d944c0cb64fec29-d17390d3fe719a93-00",
    "errorMessage": [
        "Task with identifier [1] has assigned one or more Task with identifiers: [4]"
    ]
}
```

</td>
</tr>
</table>
