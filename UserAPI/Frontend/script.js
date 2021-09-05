const form = document.forms.newUserForm;
form.addEventListener("submit", (ev) => {
    ev.preventDefault();
    // form.
    const formData = new FormData(form);

    fetch("https://localhost:44382/api/user/create", {
        body: formData,
        method: "post",
        mode: "cors"
    })
        .then((x) => {
            console.log(x);
            return x.json();
        })
        .then((x) => console.log(x));
});
