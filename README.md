# CountryDetails

Kindly test using Postman, to authenticate:

- create a post request

- point to this url: https://localhost:44338/api/token

- use this email and password object in the body

{
    "Email":"admin@admin.com",
    "Password":"PasswordA"
}

- copy the return token 

- create a get request using this url https://localhost:44338/api/Countries/{countryCode}

- add the token in the request header (prefix it with Bearer, so it will be Bearer <your token>

For any questions please email me: manal.awad.dev@gmail.com
