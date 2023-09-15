let homeApp;

axios.get('/book')
    .then((response) => {
        console.log(response);
    })
    .catch((e) => {
        //ignore
    })

homeApp = new Vue({
    el: '#home-app',
    data: {
        message: 'Hello Vue!'
    },
    methods: {
        setMessage: function (event) {
            this.message = event.target.value;
        }
    }
});