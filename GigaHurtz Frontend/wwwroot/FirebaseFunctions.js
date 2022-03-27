(function() {
    window.FirebaseFunctions = {
        signup: async function(username, password) {
            var uid = "";
            await FirebaseFunctions.auth().createUserWithEmailAndPassword(username, pass)
        }
    }
})