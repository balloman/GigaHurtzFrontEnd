(function() {
    window.FirebaseFunctions = {
        signup: async function(username, password) {
            var uid = "";
            await FirebaseFunctions.auth().createUserWithEmailAndPassword(username, password)
                .then((userCredential) => {
                    uid = userCredential.user.uid;
                });
            return uid;
        },
        login: async function(username, pass) {
            let uid = "";
            await firebase.auth().signInWithEmailAndPassword(username, pass)
                .then((userCredential) => {
                    console.log(userCredential.user.uid)
                    uid = userCredential.user.uid;
                })
                .catch((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log(errorCode, errorMessage)
                })
            return uid;
        }
    }
})