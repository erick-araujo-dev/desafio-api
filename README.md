# SorteOnlineDesafioApi Documentation

## Overview

SorteOnlineDesafioApi is an API designed to manage clients and orders for a store.

## Base URL

The base URL for all endpoints is `https://localhost:7131/`.

## Authentication

Authentication is required for accessing certain endpoints.

## Controllers

## AuthController

### Sign In

- **Method:** POST
- **Endpoint:** /api/auth/signin
- **Description:** Authenticates a user with email and password.
- **Request Body:**
	```
		{
		  "email": "string",
		  "password": "string"
		}
	```
- **Response:**
  - Status Code 200 OK
  - Returns a token for authentication.

### Sign Up

- **Method:** POST
- **Endpoint:** /api/auth/signup
- **Description:** Registers a new user with name, email, and password.
- **Request Body:**
  - `name` (string): The name of the user.
  - `email` (string): The email address of the user.
  - `password` (string): The password of the user.
- **Response:**
  - Status Code 201 Created
  - Returns a token for authentication and the ID of the newly created user.

## UserController

### Get All Users

- **Method:** GET
- **Endpoint:** /api/usuario/all
- **Description:** Retrieves all users.
- **Response:**
  - Status Code 200 OK
  - Returns a list of all users.

### Get User by ID

- **Method:** GET
- **Endpoint:** /api/usuario/{id}
- **Description:** Retrieves a user by their ID.
- **Parameters:**
  - `id` (integer): The ID of the user to retrieve.
- **Response:**
  - Status Code 200 OK
  - Returns the user information.

## StoreController

### Create Client

- **Method:** POST
- **Endpoint:** /api/store/client/create
- **Description:** Creates a new client with the provided name and email.
- **Request Body:**
  - `name` (string): The name of the client.
  - `email` (string): The email address of the client.git status
- **Response:**
  - Status Code 201 Created
  - Returns the newly created client.

### Get Client by ID

- **Method:** GET
- **Endpoint:** /api/store/client/{clientId}
- **Description:** Retrieves a client by their ID.
- **Parameters:**
  - `clientId` (integer): The ID of the client to retrieve.
- **Response:**
  - Status Code 200 OK
  - Returns the client information.

### Get All Clients

- **Method:** GET
- **Endpoint:** /api/store/client/all
- **Description:** Retrieves all clients.
- **Response:**
  - Status Code 200 OK
  - Returns a list of all clients.

### Create Order

- **Method:** POST
- **Endpoint:** /api/store/order/create
- **Description:** Creates a new order for a client.
- **Request Body:**
  - `clientId` (integer): The ID of the client placing the order.
  - `totalValue` (decimal): The total value of the order.
- **Response:**
  - Status Code 201 Created
  - Returns the newly created order.

### Get Order by ID

- **Method:** GET
- **Endpoint:** /api/store/order/{orderId}
- **Description:** Retrieves an order by its ID.
- **Parameters:**
  - `orderId` (integer): The ID of the order to retrieve.
- **Response:**
  - Status Code 200 OK
  - Returns the order information.

### Get All Orders

- **Method:** GET
- **Endpoint:** /api/store/order/all
- **Description:** Retrieves all orders.
- **Response:**
  - Status Code 200 OK
  - Returns a list of all orders.

### Delete Order

- **Method:** DELETE
- **Endpoint:** /api/store/order/{orderId}
- **Description:** Deletes an order by its ID.
- **Parameters:**
  - `orderId` (integer): The ID of the order to delete.
- **Response:**
  - Status Code 204 No Content

### Create Client and Order

- **Method:** POST
- **Endpoint:** /api/store/client-order/create
- **Description:** Creates a new client and places an order for them.
- **Request Body:**
  - `name` (string): The name of the client.
  - `email` (string): The email address of the client.
  - `totalValue` (decimal): The total value of the order.
- **Response:**
  - Status Code 201 Created
  - Returns the newly created client and order details.

### Get Client with All Orders by ID

- **Method:** GET
- **Endpoint:** /api/store/client-order/{clientId}
- **Description:** Retrieves a client by ID along with all their orders.
- **Parameters:**
  - `clientId` (integer): The ID of the client to retrieve.
- **Response:**
  - Status Code 200 OK
  - Returns the client information along with all associated orders.

### Delete Client and All Their Orders

- **Method:** DELETE
- **Endpoint:** /api/store/client-order/{clientId}
- **Description:** Deletes a client by ID along with all their associated orders.
- **Parameters:**
  - `clientId` (integer): The ID of the client to delete.
- **Response:**
  - Status Code 204 No Content

