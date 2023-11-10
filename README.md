![tests](https://github.com/antoine2116/AnagramWS/actions/workflows/tests.yml/badge.svg)
[![codecov](https://codecov.io/gh/antoine2116/AnagramWS/graph/badge.svg?token=E35WE8EF7Y)](https://codecov.io/gh/antoine2116/AnagramWS)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# Simple Anagram API

This is a simple anagram API that allows you to add words to a dictionary and 
check if a word is an anagram of any words in the dictionary.

## Getting Started

### Prerequisites

This project requires .NET 6.0 and Docker to run.

### Running the API

To run the API, run the following commands in the root directory of the project:

- Build the API:

```sh
docker build -t anagram-ws:lastest .
```

- Run the API:

```sh
docker run -d -p 5000:5000 --name anagram-ws anagram-ws:latest
```

This will build the API and run it in a Docker container. The API will be
available at http://localhost:5000.

### Running the Tests

To run the tests, run the following command in the root directory of the project:

```
dotnet test
```

## API Documentation

### Find

Returns a list of anagrams of a word.

**URL** : `/anagram/find/[word]

**Method** : `GET`

**Data constraints**

Provide the word to find anagrams of in the URL.

**Url example**

```
/anagram/find/chien
```
#### Success Response

**Code** : `200 OK`

**Content example**

```json
[
    "chine",
    "niche"
]
```
#### Error Response

** Condition** : If an error occurs.

**Code** : `500 Internal Server Error`

### Add

Adds a word to the dictionary.

**URL** : `/anagram/add`

**Method** : `POST`

**Data constraints**

Provide the word to add in the request body.

```json
{
    "word": "[word to add]"
}
```

**Data example**

```json
{
    "word": "chien"
}
```
#### Success Response

**Code** : `201 Created`

**Content example**

```json
{
    "message": "The word 'chien' was added to the dictionary."
}
```
#### Error Response

**Condition** : If the word is already in the dictionary.

**Code** : `400 Bad Request`

**Condition** : If the word is empty or missing

**Code** : `400 Bad Request`

**Condition** : If an error occurs.

**Code** : `500 Internal Server Error`

### Remove

Removes a word from the dictionary.

**URL** : `/anagram/remove`

**Method** : `POST`

**Data constraints**

Provide the word to remove in the request body.

```json
{
    "word": "[word to remove]"
}
```

**Data example**

```json
{
    "word": "chien"
}
```

#### Success Response

**Code** : `200 OK`

**Content example**

```json
{
    "message": "The word 'chien' was removed from the dictionary."
}
```

#### Error Response

**Condition** : If the word is not in the dictionary.

**Code** : `400 Bad Request`

**Condition** : If the word is empty or missing

**Code** : `400 Bad Request`

**Condition** : If an error occurs.

**Code** : `500 Internal Server Error`

### Count
Count the number of words in the dictionary that have anagrams.

**URL** : `/anagram/count`

**Method** : `POST`

**Data constraints**

None

#### Success Response

**Code** : `200 OK`

**Content example**

```json
{
    "count": 2
}
```

#### Error Response

**Condition** : If an error occurs.

**Code** : `500 Internal Server Error`

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
