function updateQuantities() {
    
    let updateTimer;
    const errorFieldForCount = document.querySelector("span[name='Count']")
    
    document.querySelectorAll(".quantity-input").forEach(input => {
        input.addEventListener("input", function (e) {
            clearTimeout(updateTimer);
            errorFieldForCount.textContent = "";  // Clear previous error message
            
            let cartId = document.querySelector("input[name='cartId']").value;
            let gameId = document.querySelector("input[name='gameId']").value;
            let newQuantity = e.target.value;
            let url = window.cartUpdateUrl;
            
            console.log(errorFieldForCount);
            
            updateTimer = setTimeout(() => {
                fetch(url, {
                    method : 'POST',
                    headers: {
                        "Content-Type": "application/json",
                        "RequestVerificationToken": getAntiForgeryToken()
                    },
                    body: JSON.stringify({CartId : cartId, GameId : gameId, Quantity: newQuantity})
                })
                .then(response => response.json())
                .then(data => {
                    if(data.success) {
                        console.log("Cart update successful")
                    } else {
                        errorFieldForCount.textContent = data.message;
                    }
                })
                .catch(error => console.error(error))
            }, 1000)
            
        })
    })
    
    
}

function getAntiForgeryToken() {
    let tokenMeta = document.querySelector("input[name='__RequestVerificationToken']");
    return tokenMeta ? tokenMeta.value : ""; 
}

document.addEventListener('DOMContentLoaded', updateQuantities);