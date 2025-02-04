function updateQuantities() {
    
    let updateTimer;
    document.querySelectorAll(".quantity-input").forEach(input => {
        input.addEventListener("input", function (e) {
            clearTimeout(updateTimer);
            const errorFieldForCount = e.target.parentElement.querySelector("span[name='Count']");
            errorFieldForCount.textContent = "";  // Clear previous error message
            
            let cartId = e.target.closest(".item-info").querySelector("input[name='cartId']").value;
            let gameId = e.target.closest(".item-info").querySelector("input[name='gameId']").value;
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
                        errorFieldForCount.textContent = data.Quantity[0]
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