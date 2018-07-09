**Backend**

The backend API will provide the following features/methods:

* /api/users/login (POST) - Pass in Email and Password and validate
* /api/users/signup (POST) - Pass in User info and Store in DB.
* /api/users (GET) - Return all Users
* /api/users/{id} (GET) - Return a given User
* /api/users/reset (POST) - Reset Password (From Email)
* /api/users/forgot (POST) - Forgot Password (Send Email)