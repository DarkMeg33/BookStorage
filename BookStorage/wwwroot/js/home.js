let homeApp;

axios.get('/books')
    .then((response) => {
        console.log(response);
    });

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