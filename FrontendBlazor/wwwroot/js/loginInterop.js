window.loginInterop = {
    login: function (loginData) {
        return new Promise((resolve, reject) => {
            console.log("por aqui");
            // Make your AJAX request here
            $.ajax({
                type: 'POST',
                url: 'https://http://localhost:7073/api/Account/Login',
                dataType: 'json',
                data: JSON.stringify(loginData),
                contentType: 'application/json',
                headers: {
                    'ApiKey': 'ec6f9d11' // Include your API key here
                },
                success: function (response) {
                    resolve(response.token);
                },
                error: function (ex) {
                    reject(ex);
                }
            });
        });
    }
};
