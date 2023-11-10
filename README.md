# Simple Anagram API

This is a simple anagram API that allows you to add words to a dictionary and 
check if a word is an anagram of any words in the dictionary.

## Getting Started

### Prerequisites

This project requires .NET 6.0 and Docker to run.

### Running the API

To run the API, run the following command in the root directory of the project:

```
docker-compose up
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

**Content example**

```json
{
    "message": "An error occurred while finding the anagrams."
}
```


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

**Content example**

```json
{
    "message": "The word 'chien' is already in the dictionary."
}
```

**Condition** : If the word is empty or missing

**Code** : `400 Bad Request`

**Content example**

```json
{
    "message": "The word cannot be empty."
}
```

**Condition** : If an error occurs.

**Code** : `500 Internal Server Error`

**Content example**

```json
{
    "message": "An error occurred while adding the word."
}
```

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

**Content example**

```json
{
    "message": "The word 'chien' is not in the dictionary."
}
```

**Condition** : If the word is empty or missing

**Code** : `400 Bad Request`

**Content example**

```json
{
    "message": "The word cannot be empty."
}
```

**Condition** : If an error occurs.

**Code** : `500 Internal Server Error`

**Content example**

```json
{
    "message": "An error occurred while removing the word."
}
```

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

**Content example**

```json
{
    "message": "An error occurred while counting the anagrams."
}
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
```
