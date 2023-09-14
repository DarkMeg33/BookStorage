let homeApp;

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