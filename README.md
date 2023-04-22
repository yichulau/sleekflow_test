To run the project, first clone the project

After cloning the project, first run the frontend side:

FRONTEND:

Access to this directory:
../TodoList.Client/my-app

build the image:
docker build -t my-app-client .

Run the container:
docker run -dp 3005:3000 my-app-client

Access the frontend with this url: http://localhost:3005/


BACKEND:

Access to this directory:
../ToDo/TodoList.Api


Run the container:
docker build -t my-backend -f TodoList.Api/Dockerfile .

Access the backend with this url: http://localhost:8080/api/todos
