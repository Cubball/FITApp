# FITApp React Web App
This is the frontend application for the FITApp.

## Running locally
### Prerequisites
- Yarn

### How to run
1. Clone the repository
    ```
    git clone https://github.com/Cubball/FITApp
    ```
1. cd into this folder
    ```
    cd FITApp/frontend
    ```
1. Create a ```.env``` file in this folder containing the BACKEND_URL environment variable
    ```bash
    BACKEND_URL=https://localhost:7000
    ```
    https://localhost:7000 is the URL of the [Gateway](../FITApp.Gateway) when launched locally with default configuration. You can change it if needed.
1. Install the dependencies
    ```
    yarn install
    ```
1. Run the app
    ```
    yarn run dev
    ```
